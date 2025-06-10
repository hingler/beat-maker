using beatmaker.beatmap;
using beatmaker.input;
using beatmaker.input.impl;
using beatmaker.player;
using beatmaker.player.impl;
using beatmaker.render;
using beatmaker.render.impl;
using beatmaker.time;
using beatmaker.time.timer;

namespace beatmaker.engine.impl;

public class SimpleBeatEngine : IBeatEngine {
  private readonly IBeatMap source_map;
  private readonly SimpleBeatPlayer player;
  private readonly SimpleInputHandler input_handler;
  private readonly SimpleBeatRender beat_render;

  private readonly SimpleTimer timer;

  public SimpleBeatEngine(
    IBeatMap map,
    IBeatClock clock
  ) {
    source_map = map;
    player = new(source_map);

    input_handler = new SimpleInputHandler(player);
    beat_render = new SimpleBeatRender(player);

    timer = new SimpleTimer(clock);
  }

  public IBeatRender GetBeatRender() => beat_render;
  public void SendInput(int column, bool pressed) {
    Update();

    if (pressed) {
      input_handler.Press(column);
    }
    else {
      input_handler.Release(column);
    }
  }

  public void Update() {
    player.PlayheadPosition = timer.GetInterval();
  }

  public void Play() {
    timer.StartTimer();
  }

  public void Pause() {
    timer.StopTimer();
  }
}