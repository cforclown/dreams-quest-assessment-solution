using PlayerSystem;

public class PlayerStateIdle : IState<PlayerStates> {
  private readonly PlayerStates STATE = PlayerStates.Idle;

  public PlayerStates State => STATE;

  public void Enter() {

  }

  public void Exit() {

  }

  public void Update() {

  }
}
