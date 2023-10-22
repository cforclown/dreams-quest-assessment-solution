namespace PlayerSystem {
  public enum PlayerStates {
    Idle,
    Attacking,
  }

  public class PlayerStatesInstances {
    public PlayerStateIdle IdleState { get; }
    public PlayerStateAttacking AttackingState { get; }

    public PlayerStatesInstances(
      PlayerStateIdle idleState,
      PlayerStateAttacking attackingState
    ) {
      this.IdleState = idleState;
      this.AttackingState = attackingState;
    }

    public IState<PlayerStates> GetState(PlayerStates gameState) {
      return gameState switch {
        PlayerStates.Idle => IdleState,
        PlayerStates.Attacking => AttackingState,
        _ => null
      };
    }
  }
}
