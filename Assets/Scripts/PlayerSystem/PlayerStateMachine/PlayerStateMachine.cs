using System;
using UnityEngine;

namespace PlayerSystem {
  public class PlayerStateMachine : StateMachine<PlayerStates>, IOnStateChanged<PlayerStates> {
    private PlayerStatesInstances stateInstances;

    public event Action<PlayerStates, PlayerStates> OnStateChanged;

    public override void Update() {
      Current?.Update();
    }

    public void ChangeState(PlayerStates newState) {
      PlayerStates prevState = Current.State;
      base.ChangeState(stateInstances.GetState(newState));
      Debug.Log($"Player: {newState}");
      OnStateChanged(newState, prevState);
    }

    public void SetStateInstances(PlayerStatesInstances stateInstances) => this.stateInstances = stateInstances;
  }
}
