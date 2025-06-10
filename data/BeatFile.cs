namespace beatmaker.data;

public struct BeatFile {
  public string Name;
  public string Description;

  // audio data, offsets

  public List<TimePoint> TimePoints;
  public List<HitMarker> HitMarkers;
}