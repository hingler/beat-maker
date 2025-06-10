using beatmaker.beatmap;
using beatmaker.player;

namespace beatmaker.input.impl;

public class SimpleInputHandler(IBeatPlayer player) : IInputHandler {
  private readonly Dictionary<int, Note> last_held_note = [];

  // score the closer note tbh
  private static readonly double MAX_PRESS_WINDOW = 0.25;

  public void Press(int column) {
    if (last_held_note.ContainsKey(column)) {
      // sub frame tap - should never happen but just in case!!!
      Release(column);
    }

    IList<Note> notes = player.GetUpcomingNotes(MAX_PRESS_WINDOW);


    double closest_note = MAX_PRESS_WINDOW + 0.01;
    Note min_note = null;
    foreach (Note note in notes) {
      double delta = Math.Abs(player.PlayheadPosition - note.Data.Sec);
      if (
        note.Data.Offset == column &&
        delta < closest_note
      ) {
        min_note = note;
        closest_note = delta;
      }
    }

    if (min_note != null) {
      last_held_note.Add(column, min_note);
      player.HitNote(min_note);
    }
  }

  public void Release(int column) {
    if (last_held_note.TryGetValue(column, out Note output)) {
      player.ReleaseNote(output);
      last_held_note.Remove(column);
    }
  }
}