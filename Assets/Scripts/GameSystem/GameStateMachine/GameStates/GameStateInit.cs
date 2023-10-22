using BlockSystem;
using GameEventSystem;
using PlayerSystem;

namespace GameSystem {
  public class GameStateInit : IState<GameStates> {
    private readonly GameStates STATE = GameStates.Init;

    public GameStates State => STATE;

    private GameStateMachine gameStateMachine;
    private PlayerModelInstance playerData;
    private MapModelInstance mapData;
    private IObserver observer;

    public GameStateInit(
      GameStateMachine gameStateMachine,
      PlayerModelInstance playerData,
      MapModelInstance mapData,
      IObserver observer
    ) {
      this.gameStateMachine = gameStateMachine;
      this.playerData = playerData;
      this.mapData = mapData;
      this.observer = observer;
    }

    public void Enter() {
      LoadSavedData();
    }

    public void Exit() {
    }

    public void Update() {

    }

    private void LoadSavedData() {
      playerData.Data = MockServer.LoadPlayerData();
      mapData.Data = MockServer.LoadMapData();
      observer.NotifyObservers(new PlayerDataSignal(playerData.Data));
    }
  }
}
