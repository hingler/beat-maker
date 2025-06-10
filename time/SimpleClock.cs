using Godot;

namespace beatmaker.time;

public class SimpleClock : IBeatClock {
  public ulong GetTimestampUs() {
    return Time.GetTicksUsec();
  }
}