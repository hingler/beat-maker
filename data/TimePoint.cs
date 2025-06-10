namespace beatmaker.data;

// riffed from osu format lul

public struct TimePoint : IBeatEvent {
  public double Sec { get; set; }

  // bpm of this section
  public double BPM;
  public double BeatLength {
    readonly get => 60 / BPM;
    set { BPM = 60 / value; }
  }

  // offset of downbeat for this timing point
  public double BeatOffsetMsec;

  // beats per measure
  public double Meter;

  // references to hit sounds, etc.
}