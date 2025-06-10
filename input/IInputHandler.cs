namespace beatmaker.input;

public interface IInputHandler {
  public void Press(int column);
  public void Release(int column);

  // figure out which note is "closest"
  // send hit to beat player, if there's a relevant one

  // also: take inputs from notes that are older than (say) 0.5s (miss)
}