using BlockSystem;
using GameEventSystem;
using PlayerSystem;
using System;
using VContainer;
using VContainer.Unity;

namespace GameSystem {
  public class GameStateManager : IStartable, IDisposable {
    public static GameStateManager I;

    public IObserver Observer { get; private set; }
    public PlayerModelInstance Player { get; private set; }
    public MapModelInstance MapData { get; private set; }
    public GameStateMachine GameStateMachine { get; private set; }
    public GameStatesInstances GameStatesInstances { get; private set; }
    public GameSceneManager GameSceneManager { get; private set; }

    public GameStates State {
      get {
        return GameStateMachine.Current.State;
      }
    }

    [Inject]
    public GameStateManager(
      IObserver observer,
      PlayerModelInstance player,
      MapModelInstance mapData,
      GameStateMachine gameStateMachine,
      GameStatesInstances gameStatesInstances
    ) {
      I = this;

      this.Observer = observer;
      this.Player = player;
      this.MapData = mapData;
      this.GameStateMachine = gameStateMachine;
      this.GameStatesInstances = gameStatesInstances;

      GameStateMachine.SetStatesInstances(this.GameStatesInstances);
      GameStateMachine.OnStateChanged += OnStateChanged;

      GameStateMachine.ChangeState(GameStates.Init);
    }

    public void Start() {
      Observer.AddObserver<PlayerDataSignal>(SavePlayerData);
      Observer.AddObserver<MapDataSignal>(SaveMapData);
      GameStateMachine?.ChangeState(GameStates.MainMenu);
    }

    public void Dispose() {
      Observer.RemoveObserver<PlayerDataSignal>(SavePlayerData);
      Observer.RemoveObserver<MapDataSignal>(SaveMapData);
      GameStateMachine.OnStateChanged -= OnStateChanged;
    }

    public void ChangeState(GameStates gameStates) {
      GameStateMachine.ChangeState(gameStates);
    }

    private void SavePlayerData(PlayerDataSignal signal) {
      MockServer.SavePlayerData(signal.PlayerData);
    }

    private void SaveMapData(MapDataSignal signal) {
      MockServer.SaveMapData(signal.Data);
    }

    private void OnStateChanged(GameStates newState, GameStates prevState) {
      Observer.NotifyObservers(new GameStateChangedSignal(newState, prevState));
    }
  }
}
