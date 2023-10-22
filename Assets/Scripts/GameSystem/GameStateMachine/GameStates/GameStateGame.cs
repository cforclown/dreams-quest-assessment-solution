namespace GameSystem {
  public class GameStateGame : IState<GameStates> {
    private readonly GameStates STATE = GameStates.Game;
    public GameStates State => STATE;

    private GameStateMachine gameStateMachine;

    public GameStateGame(GameStateMachine gameStateMachine) {
      this.gameStateMachine = gameStateMachine;
    }

    public void Enter() {

    }

    public void Exit() {

    }

    public void Update() {

    }
  }
}
