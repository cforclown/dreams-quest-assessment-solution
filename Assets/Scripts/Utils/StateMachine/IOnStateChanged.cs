using System;

public interface IOnStateChanged<T> {
  public event Action<T, T> OnStateChanged;
}
