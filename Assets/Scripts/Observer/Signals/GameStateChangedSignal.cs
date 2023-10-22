using GameSystem;

public struct GameStateChangedSignal {
  public GameStates NewState;
  public GameStates PrevState;

  public GameStateChangedSignal(
    GameStates newState,
    GameStates prevState
  ) {
    this.NewState = newState;
    this.PrevState = prevState;
  }
}
