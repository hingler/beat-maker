
namespace beatmaker.data;

public struct HitMarker : IBeatEvent {
  public double Sec { get; set; }

  // x offset of marker
  public int Offset;

  // if > 0, creates a hold (ie how long to hold for)
  public double SecHold;

  // tba: info on which samples to play, etc...
}