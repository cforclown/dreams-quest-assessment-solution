using BlockSystem;
using GameEventSystem;
using GameSystem;
using MainMenuScene;
using PlayerSystem;
using VContainer;
using VContainer.Unity;

namespace Root {
  public class GameLifetime : LifetimeScope {
    public static GameLifetime Instance;
    private static bool Configured = false;

    public MainMenuView MainMenuView;


    protected override void Awake() {
      DontDestroyOnLoad(this);
      if (Instance != null && Instance != this) {
        Destroy(gameObject);
        Destroy(this);
      }
      else {
        Instance = this;
      }

      base.Awake();
    }

    protected override void Configure(IContainerBuilder builder) {
      if (Configured) {
        return;
      }

      builder.Register<Observer>(Lifetime.Singleton).As<IObserver>();
      builder.Register<PlayerModelInstance>(Lifetime.Singleton);
      builder.Register<MapModelInstance>(Lifetime.Singleton);

      builder.RegisterEntryPoint<GameStateMachine>().As<StateMachine<GameStates>>().AsSelf();
      builder.Register<GameStatesInstances>(Lifetime.Singleton);
      builder.RegisterEntryPoint<GameSceneManager>();
      builder.Register<GameStateInit>(Lifetime.Scoped);
      builder.Register<GameStateMainMenu>(Lifetime.Scoped);
      builder.Register<GameStateGame>(Lifetime.Scoped);
      builder.Register<GameStateLoadingLevel>(Lifetime.Scoped);
      builder.Register<GameStatePause>(Lifetime.Scoped);
      builder.Register<GameStateExit>(Lifetime.Scoped);

      builder.RegisterEntryPoint<GameStateManager>().AsSelf();

      builder.RegisterComponent(MainMenuView);
      builder.RegisterEntryPoint<MainMenu>(Lifetime.Singleton);

      Configured = true;
    }
  }
}
