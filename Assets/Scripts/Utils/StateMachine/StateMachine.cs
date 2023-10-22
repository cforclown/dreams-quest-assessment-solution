public abstract class StateMachine<T> {
  public IState<T> Current { get; set; }

  public virtual void ChangeState(IState<T> state) {
    Current?.Exit();
    Current = state;
    Current?.Enter();
  }

  public virtual void Update() => Current?.Update();
}
