using GameSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystem {
  [RequireComponent(typeof(PlayerInput))]
  public class PlayerManager : MonoBehaviour {
    private PlayerInput playerInput;

    private PlayerStateMachine playerStateMachine;
    private PlayerModelInstance playerData;

    // ------------------------------------
    private readonly float movementSpd = 10.0f;
    private readonly float zoomSpd = 50f;
    private InputAction moveAction;
    // ------------------------------------

    private void Awake() {
      playerStateMachine = new PlayerStateMachine();
      playerData = GameStateManager.I?.Player;

      playerInput = GetComponent<PlayerInput>();
    }

    private void Start() {
      moveAction = playerInput.actions["Movement"];
    }

    private void Update() {
      CheckMovement();
      CheckZoom();
    }

    private void CheckMovement() {
      Vector3 cameraForward = Camera.main.transform.forward;
      cameraForward.y = 0f;
      Vector3 cameraRight = Camera.main.transform.right;
      cameraRight.y = 0f;
      Vector2 awsdInput = moveAction.ReadValue<Vector2>();
      Vector3 move = new Vector3(awsdInput.x, 0, awsdInput.y);
      if (move != Vector3.zero) {
        transform.position = transform.position + ((cameraForward * move.z + cameraRight * move.x).normalized * Time.deltaTime * movementSpd);
      }
    }

    private void CheckZoom() {
      var v = Input.GetAxis("Mouse ScrollWheel");
      if (v == 0f) {
        return;
      }

      transform.position += transform.forward * (v > 0 ? 1f : -1f) * zoomSpd * Time.deltaTime;
    }
  }
}

