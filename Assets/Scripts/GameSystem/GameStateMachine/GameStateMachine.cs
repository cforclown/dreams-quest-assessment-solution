using System;
using UnityEngine;
using VContainer.Unity;

namespace GameSystem {
  public class GameStateMachine : StateMachine<GameStates>, IOnStateChanged<GameStates>, ITickable {
    private GameStatesInstances stateInstances;

    public event Action<GameStates, GameStates> OnStateChanged;

    public void Tick() {
      Current?.Update();
    }

    public void ChangeState(GameStates newState) {
      if (Current != null && Current.State == newState) {
        return;
      }

      GameStates? preState = Current?.State;
      base.ChangeState(stateInstances.GetState(newState));
      Debug.Log($"Game state: {newState}");
      if (preState == null) {
        return;
      }
      OnStateChanged?.Invoke(newState, (GameStates)preState);
    }

    public void SetStatesInstances(GameStatesInstances stateInstances) => this.stateInstances = stateInstances;
  }
}
