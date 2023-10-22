using GameSystem;
using UnityEngine;

namespace MainSceneUI {
  [RequireComponent(typeof(GameUIView))]
  public class GameUI : MonoBehaviour {
    private GameUIView gameUIView;

    void Awake() {
      gameUIView = GetComponent<GameUIView>();
      Canvas canvas = GetComponent<Canvas>();
      canvas.renderMode = RenderMode.ScreenSpaceCamera;
      canvas.planeDistance = 0.5f;
      canvas.worldCamera = Camera.main;
    }

    void Start() {
      gameUIView.MainMenuBtn.onClick.AddListener(OnMainMenuBtnClick);
    }

    private void OnMainMenuBtnClick() {
      GameStateManager.I.ChangeState(GameStates.MainMenu);
    }
  }
}
