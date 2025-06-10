using beatmaker.beatmap;

namespace beatmaker.player;

public interface INoteListener {
  // note - note that was just hit
  // delta - offset (in secs) btwn hit time and the note's time
  public void OnHit(Note note, double delta);

  // called when a note is "held"
  // note - the note being held
  // hold_delta - the number of seconds (since last call) that the hold has been held for
  public void OnHold(Note note, double hold_delta);
}