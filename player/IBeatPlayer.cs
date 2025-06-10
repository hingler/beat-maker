// how do we ensure that beats are only played once?
// - beat player handles "hits" from the player
// - don't expose notes that have been hit

// - let the input component determine which upcoming "note" should be hit
// - give notes a unique "identifier"
// - use classes and hash-equality


// better idea

// - data -> beatmap
// - beatmap tacks on ID fields
// - sort output

using System.Collections.ObjectModel;
using beatmaker.beatmap;

namespace beatmaker.player;

public interface IBeatPlayer {
  // return upcoming notes
  // mark notes as "hit" (based on what input logic tells us)

  public double PlayheadPosition { get; }

  public IList<Note> GetUpcomingNotes(double lookahead_sec);

  // marks note as hit and generates an event
  public void HitNote(Note target);
  // add "release note" as call for holds
  // alt: hit/hold and release (hold just logs that the note is being played - hit gets treated the same)
  // alt2: hit will auto-hold
  public ReadOnlyDictionary<Note, double> GetHeldNotes();
  public void ReleaseNote(Note target);
  public void RegisterNoteListener(INoteListener listener);

  // options for holds
  // - hit -> release
  // - call hit only once the note is played (non-ideal)
  // - fire multiple hits while the hold is in progress (how?)
  //   - inputevent would take care of this
  //   - treat a hold as multiple notes (non-standard)
  //   - separate hit into "hit/release"
  //   - 
}