using GameSystem;
using UnityEngine;

namespace MainSceneUI {
  [RequireComponent(typeof(GameUIView))]
  public class GameUI : MonoBehaviour {
    private GameUIView view;

    void Awake() {
      view = GetComponent<GameUIView>();
      Canvas canvas = GetComponent<Canvas>();
      canvas.renderMode = RenderMode.ScreenSpaceCamera;
      canvas.planeDistance = 0.5f;
      canvas.worldCamera = Camera.main;
    }

    void Start() {
      view.MainMenuBtn.onClick.AddListener(OnMainMenuBtnClick);
      view.CloseTutorialBtn.onClick.AddListener(() => view.TutorialContainer.SetActive(false));

      view.TutorialContainer.SetActive(true);
    }

    private void OnDestroy() {
      view.MainMenuBtn.onClick.RemoveAllListeners();
      view.CloseTutorialBtn.onClick.RemoveAllListeners();
    }

    private void OnMainMenuBtnClick() {
      GameStateManager.I.ChangeState(GameStates.MainMenu);
    }
  }
}
