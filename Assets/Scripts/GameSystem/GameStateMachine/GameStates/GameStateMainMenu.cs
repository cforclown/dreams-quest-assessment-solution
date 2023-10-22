using GameEventSystem;
using PlayerSystem;

namespace GameSystem {
  public class GameStateMainMenu : IState<GameStates> {
    private readonly GameStates STATE = GameStates.MainMenu;
    public GameStates State => STATE;

    private readonly IObserver observer;
    private readonly PlayerModelInstance playerData;
    private readonly GameStateMachine gameStateMachine;

    public GameStateMainMenu(
      IObserver observer,
      PlayerModelInstance playerData,
      GameStateMachine gameStateMachine
    ) {
      this.observer = observer;
      this.playerData = playerData;
      this.gameStateMachine = gameStateMachine;
    }

    public void Enter() {
    }

    public void Exit() {
      observer.NotifyObservers(new PlayerDataSignal(playerData.Data));
    }

    public void Update() {

    }
  }
}
