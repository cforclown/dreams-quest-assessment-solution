using GameSystem;
using UnityEngine;

namespace MainMenuScene {
  [RequireComponent(typeof(MainMenuView))]
  public class MainMenu : MonoBehaviour {
    private MainMenuView view;

    private void Awake() {
      view = GetComponent<MainMenuView>();

      view.PlayerNameInput.text = GameStateManager.I?.Player.Data.Username;
    }

    private void Start() {
      view.PlayBtn.onClick.AddListener(OpenGameScene);
      view.ExitBtn.onClick.AddListener(Quit);
    }

    private void OnDestroy() {
      view.PlayBtn.onClick.RemoveAllListeners();
      view.ExitBtn.onClick.RemoveAllListeners();
    }

    private void OpenGameScene() {
      GameStateManager.I.Player.Data.Username = view.PlayerNameInput.text;
      GameStateManager.I.ChangeState(GameStates.Game);
    }

    private void Quit() => Application.Quit();
  }
}
