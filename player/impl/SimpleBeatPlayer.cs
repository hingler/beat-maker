using System.Collections.ObjectModel;
using beatmaker.beatmap;
using beatmaker.data;

namespace beatmaker.player.impl;

public class SimpleBeatPlayer : IBeatPlayer {
  private readonly LinkedList<Note> note_list = new();
  private double pos = 0.0;
  public double PlayheadPosition {
    get => pos;
    set {
      pos = value;
      CullMissedNotes();
    }
  }
  private static readonly double CLEANUP_INTERVAL = 5.0;

  private readonly HashSet<INoteListener> listeners = [];

  private readonly Dictionary<Note, double> held_notes = [];

  public SimpleBeatPlayer(IBeatMap map) {
    IReadOnlyList<Note> notes = map.GetAllNotes();

    // maintain order
    note_list = new(notes);
  }

  public void CullMissedNotes() {
    while ((PlayheadPosition - note_list.First.Value.Data.Sec) > CLEANUP_INTERVAL) {
      // ie: while playhead is more than (cleanup interval) secs ahead of earliest note
      note_list.RemoveFirst();
    }
  }

  public IList<Note> GetUpcomingNotes(double lookahead_sec) {
    List<Note> res = [];
    LinkedListNode<Note> node = note_list.First;
    while (
      node != null &&
      node.Value.Data.Sec < (PlayheadPosition + lookahead_sec)
    ) {
      res.Add(node.Value);
      node = node.Next;
    }

    return res;
  }

  // for holds: mark the exact playhead at which the "hit" was received
  public void HitNote(Note target) {
    LinkedListNode<Note> node = note_list.Find(target);
    if (node != null) {
      // hit - so remove

      Note note = node.Value;
      HitMarker data = note.Data;
      note_list.Remove(node);
      // handle hit event
      double delta_sec = PlayheadPosition - data.Sec;
      foreach (INoteListener listener in listeners) {
        listener.OnHit(note, delta_sec);
      }

      if (data.SecHold > 0) {
        HoldNote(note);
      }
    }
  }

  private void HoldNote(Note target) {
    double hold_start = target.Data.Sec;
    held_notes.Add(target, hold_start);
  }

  // hold start time
  public ReadOnlyDictionary<Note, double> GetHeldNotes() => held_notes.AsReadOnly();

  // alt3: use some "time-keeper" to track exactly when hit/hold calls are made
  public void ReleaseNote(Note target) {
    held_notes.Remove(target);
  }

  public void RegisterNoteListener(INoteListener listener) {
    listeners.Add(listener);
  }
}