namespace beatmaker.time.timer;

public class SimpleTimer(IBeatClock clock) {

  private double time_elapsed = 0.0;
  private bool running = false;

  private ulong last_timestamp_us = 0;

  public double StartTimer() {
    UpdateTimer();

    last_timestamp_us = clock.GetTimestampUs();
    running = true;

    return time_elapsed;
  }

  public double StopTimer() {
    UpdateTimer();

    running = false;
    return time_elapsed;
  }

  // return time interval in seconds
  public double GetInterval() {
    UpdateTimer();

    return time_elapsed;
  }

  public void ResetTimer() {
    StopTimer();
    
    time_elapsed = 0.0;
    running = false;
  }

  private void UpdateTimer() {
    ulong current_timestamp = clock.GetTimestampUs();

    if (running) {
      ulong interval_us = current_timestamp - last_timestamp_us;
      time_elapsed += interval_us / 1_000_000D;
    }

    last_timestamp_us = current_timestamp;
  }
}