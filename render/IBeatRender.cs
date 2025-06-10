namespace beatmaker.render;

public interface IBeatRender {


  public int WindowHeight { get; set; }

  // speed at which notes approach. think we'll do something like "px/s times some fac"
  public int NoteSpeed { get; set; }

  // width of each column, in px
  public int ColumnWidthPx { get; set; }

  // position of the hitmark, relative to the window size
  public double HitmarkOffset { get; set; }

  // fetch all visible notes (based on speed, window height)
  public IEnumerable<NoteMarker> GetVisibleNotes();

  // holds render as lines (underneath notes)
  public IEnumerable<HoldMarker> GetVisibleHolds();
}