using beatmaker.data;

namespace beatmaker.beatmap;

public interface IBeatMap {

  // returns a read-only list of all notes in the track, sorted by start time
  IReadOnlyList<Note> GetAllNotes();

  // returns a read-only list of all time points in the track, sorted by start time
  // ignore these for now - treat them like syntactic sugar
  IReadOnlyList<TimePoint> GetAllTimePoints();
}