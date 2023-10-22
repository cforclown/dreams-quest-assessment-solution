public interface IState<T> {
  public void Enter();
  public void Exit();
  public void Update();

  public T State { get; }
}