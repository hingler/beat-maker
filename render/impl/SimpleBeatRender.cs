using beatmaker.beatmap;
using beatmaker.data;
using beatmaker.player;
using Godot;

namespace beatmaker.render.impl;

public class SimpleBeatRender(IBeatPlayer player) : IBeatRender {
  public int WindowHeight { get; set; }
  public int NoteSpeed { get; set; }
  public int ColumnWidthPx { get; set; }
  public double HitmarkOffset { get; set; }

  private static readonly int PX_PER_SECOND = 32;

  private double GetLookaheadSecs() {
    double window_span = WindowHeight / (NoteSpeed * PX_PER_SECOND);
    return window_span * HitmarkOffset + 0.5;
  }

  private double GetLookbehindSecs() {
    double window_span = WindowHeight / (NoteSpeed * PX_PER_SECOND);
    return window_span * (1.0 - HitmarkOffset);
  }

  private float GetYOffset(double time_sec) {
    double time_delta = time_sec - player.PlayheadPosition;
    return (float)(HitmarkOffsetPx - (time_delta * NoteIntervalSizePx));
  }

  private float GetXOffset(int column) {
    return (float)(ColumnWidthPx * (column + 0.5));
  }

  public IEnumerable<NoteMarker> GetVisibleNotes() {
    double lookahead = GetLookaheadSecs() * 1.5;

    IList<Note> notes = player.GetUpcomingNotes(lookahead);
    List<NoteMarker> note_markers = [];

    foreach (Note note in notes) {
      // time until note reaches hitmark
      HitMarker hit = note.Data;
      // top = 0
      float note_y = GetYOffset(hit.Sec);
      float note_x = GetXOffset(hit.Offset);
      NoteMarker marker = new() {
        NotePos = new(note_x, note_y),
        Note = note
      };

      note_markers.Add(marker);
    }

    return note_markers;
  }

  public IEnumerable<HoldMarker> GetVisibleHolds() {
    IEnumerable<NoteMarker> note_markers = GetVisibleNotes();
    List<HoldMarker> holds = [];
    foreach (NoteMarker marker in note_markers) {
      HitMarker data = marker.Note.Data;
      if (data.SecHold > 0) {
        float hold_start = GetYOffset(data.Sec);
        float hold_end = GetYOffset(data.Sec + data.SecHold);
        float hold_x = GetXOffset(data.Offset);

        holds.Add(new HoldMarker() {
          HoldStart = new(hold_x, hold_start),
          HoldEnd = new(hold_x, hold_end)
        });
      }
    }

    return holds;
  }

  private int HitmarkOffsetPx => (int)Math.Round(WindowHeight * HitmarkOffset);
  private double NoteIntervalSizePx => NoteSpeed * PX_PER_SECOND;
}