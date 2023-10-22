using VContainer;

namespace GameSystem {
  public class GameStatesInstances {
    public GameStateInit InitState { get; }
    public GameStateMainMenu MainMenuState { get; }
    public GameStateGame GameState { get; }
    public GameStateLoadingLevel LoadingLevelState { get; }
    public GameStatePause PauseState { get; }
    public GameStateExit ExitState { get; }

    [Inject]
    public GameStatesInstances(
      GameStateInit gameStateInit,
      GameStateMainMenu gameStateMenu,
      GameStateGame gameStateGame,
      GameStateLoadingLevel gameStateLoadingLevel,
      GameStatePause gameStatePause,
      GameStateExit gameStateExit
    ) {
      this.InitState = gameStateInit;
      this.MainMenuState = gameStateMenu;
      this.GameState = gameStateGame;
      this.LoadingLevelState = gameStateLoadingLevel;
      this.PauseState = gameStatePause;
      this.ExitState = gameStateExit;
    }

    public IState<GameStates> GetState(GameStates gameState) {
      return gameState switch {
        GameStates.Init => InitState,
        GameStates.MainMenu => MainMenuState,
        GameStates.Game => GameState,
        GameStates.LoadingLevel => LoadingLevelState,
        GameStates.Pause => PauseState,
        GameStates.Exit => ExitState,
        _ => null
      };
    }
  }
}
