using beatmaker.render;

namespace beatmaker.engine;

// coreograph btwn our different components here

public interface IBeatEngine {
  public IBeatRender GetBeatRender();
  // audio component
  public void SendInput(int column, bool pressed);

  // get more accurate timestamps from the renderer
  public void Update();

  // queue playback
  public void Play();
  
  // pause playback
  public void Pause();
}