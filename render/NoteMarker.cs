using System.Numerics;
using beatmaker.beatmap;

namespace beatmaker.render;

public struct NoteMarker {
  // px
  public Vector2 NotePos;
  public Note Note;
}

public struct HoldMarker {
  // both px
  public Vector2 HoldStart;
  public Vector2 HoldEnd;

  // dont worry abt thickness - let the renderer handle it
}