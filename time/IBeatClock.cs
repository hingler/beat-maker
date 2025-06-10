namespace beatmaker.time;

public interface IBeatClock {
  // fetch timestamp in microseconds from arb point
  public ulong GetTimestampUs();
}