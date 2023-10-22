using System;
using System.Collections.Generic;

namespace GameEventSystem {
  public interface IObserver {
    public void AddObserver<T>(Action<T> callback);
    public void RemoveObserver<T>(Action<T> callback);
    public void NotifyObservers<T>(T payload);
  }

  public class Observer : IObserver {
    private readonly Dictionary<Type, object> observers = new();

    public void AddObserver<T>(Action<T> callback) {
      object callbacks = observers.GetValueOrDefault(typeof(T), null);
      if (callbacks is null) {
        callbacks = new List<Action<T>>();
        observers[typeof(T)] = callbacks;
      }

      ((List<Action<T>>)callbacks).Add(callback);
    }

    public void RemoveObserver<T>(Action<T> callback) {
      if (!observers.ContainsKey(typeof(T))) {
        return;
      }

      ((List<Action<T>>)observers[typeof(T)]).Remove(callback);
    }

    public void NotifyObservers<T>(T payload) {
      object callbacks = observers.GetValueOrDefault(typeof(T), null);
      if (callbacks is null) {
        return;
      }

      foreach (Action<T> callback in (List<Action<T>>)callbacks) {
        callback(payload);
      }
    }
  }
}
