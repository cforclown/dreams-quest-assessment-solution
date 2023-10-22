using UnityEngine;
using UnityEngine.UI;

public class GameUIView : MonoBehaviour {
  [field: SerializeField] public Button MainMenuBtn { get; private set; }
  [field: SerializeField] public BlockPlaceholder Block1Placeholder { get; private set; }
  [field: SerializeField] public BlockPlaceholder Block2Placeholder { get; private set; }
  [field: SerializeField] public BlockPlaceholder Block3Placeholder { get; private set; }
}
