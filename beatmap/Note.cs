using beatmaker.data;

namespace beatmaker.beatmap;

public class Note(int id, HitMarker data) : IEquatable<Note> {
  public long ID { get => id; }
  public HitMarker Data => data;

  public override bool Equals(object obj) {
    if (obj is Note note) {
      return note.ID == ID;
    }

    return false;
  }

  public override int GetHashCode() {
    return (ID.GetHashCode() * 31) ^ Data.GetHashCode();
  }

  public bool Equals(Note obj) {
    return (obj != null && obj.ID == ID);
  }
}