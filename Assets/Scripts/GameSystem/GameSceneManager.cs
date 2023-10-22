using GameEventSystem;
using System;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace GameSystem {
  public interface IGameSceneManager {
    public void ChangeScene(string sceneName);
  }

  public class GameSceneManager : IGameSceneManager, IStartable, IDisposable {
    private readonly GameStateMachine gameStateMachine;
    private readonly GameStatesInstances gameStatesInstances;
    private readonly IObserver observer;

    public GameSceneManager(
      GameStateMachine gameStateMachine,
      GameStatesInstances gameStatesInstances,
      IObserver observer
    ) {
      this.gameStateMachine = gameStateMachine;
      this.gameStatesInstances = gameStatesInstances;
      this.observer = observer;
    }

    public void Start() {
      observer.AddObserver<GameStateChangedSignal>(OnGameStateChanged);
    }

    public void Dispose() {
      observer.RemoveObserver<GameStateChangedSignal>(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameStateChangedSignal signal) {
      if (signal.NewState == GameStates.Game) {
        ChangeScene("Map1");
      }
      else if (signal.NewState == GameStates.MainMenu && signal.PrevState is not GameStates.Init) {
        ChangeScene("MainMenu");
      }
    }

    public void ChangeScene(string sceneName) {
      SceneManager.LoadScene(sceneName);
    }
  }
}
