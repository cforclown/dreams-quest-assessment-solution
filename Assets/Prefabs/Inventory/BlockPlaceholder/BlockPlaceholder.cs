using BlockSystem;
using GameEventSystem;
using GameSystem;
using PlayerSystem;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class BlockPlaceholder : MonoBehaviour {
  [field: SerializeField] public BlockTypes Type { get; private set; }
  private GameObject prefab;

  private Image containerImg;
  [SerializeField] private Image blockImg;
  [SerializeField] private TextMeshProUGUI qtyText;

  private IObserver observer;
  private PlayerModelInstance playerData;
  private MapModelInstance mapData;

  private Color defaultColor;
  private readonly Color onHoverColor = new Color(1f, 0.8f, 0.35f, 0.4f);

  private BlockManager draggingBlock;

  private void Awake() {
    containerImg = GetComponent<Image>();
    defaultColor = containerImg.color;

    observer = GameStateManager.I?.Observer;
    playerData = GameStateManager.I?.Player;
    mapData = GameStateManager.I?.MapData;

    LoadPrefab();
  }

  void Start() {
    blockImg.gameObject.SetActive(true);
    blockImg.sprite = BlockSpawner.LoadBlockSprite(Type);
    qtyText.gameObject.SetActive(true);

    observer?.AddObserver<PlayerDataSignal>(OnPlayerDataUpdated);

    Evaluate(playerData?.Data);
  }

  private void OnDestroy() {
    observer?.RemoveObserver<PlayerDataSignal>(OnPlayerDataUpdated);
  }

  private void OnPlayerDataUpdated(PlayerDataSignal signal) {
    Evaluate(signal.PlayerData);
  }

  private void Evaluate(PlayerModel player) {
    if (player is null) {
      return;
    }

    PlayerBlockItemModel currentBlock = player.Inventory.Blocks.Find(i => i.Type == Type);
    if (currentBlock == null) {
      qtyText.text = "0";
      return;
    }

    qtyText.text = currentBlock.Qty.ToString();
  }

  private void LoadPrefab() {
    AsyncOperationHandle<GameObject> block1Operation = Addressables.LoadAssetAsync<GameObject>(
      BlockSpawner.GetBlockPrefabNameByType(Type)
    );
    block1Operation.Completed += OnLoadBlockPrefabCompleted;
  }

  private void OnLoadBlockPrefabCompleted(AsyncOperationHandle<GameObject> asyncOperationHandle) {
    if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {
      prefab = asyncOperationHandle.Result;
      SpawnBlocksOnMap();
    }
    else {
      Debug.LogError("Block placeholder: failed to load block prefab");
    }
  }

  private void SpawnBlocksOnMap() {
    if (mapData is null) {
      return;
    }

    foreach (MapBlockModel block in mapData.Data.Blocks) {
      if (block.Block.Type != Type) {
        continue;
      }
      Instantiate(
        prefab,
        block.Position.ToVector3(),
        block.Rotation.ToQuaternion()
      );
    }
  }

  // Event trigger
  public void OnHoverStart(BaseEventData eventData) {
    containerImg.color = onHoverColor;
  }

  // Event trigger
  public void OnHoverEnd(BaseEventData eventData) {
    containerImg.color = defaultColor;
  }

  // Event trigger
  public void OnBeginDrag(BaseEventData eventData) {
    containerImg.color = onHoverColor;

    Vector3? pos = BlockSpawner.GetArenaPosByMousePos();
    pos = pos is null ? new Vector3(-1000f, -1000f, -1000f) : pos;
    GameObject blockObj = Instantiate(
      prefab,
      pos.Value,
      prefab.transform.rotation
    );
    blockObj.tag = "TempBlock";
    blockObj.layer = LayerMask.NameToLayer("TempBlock");
    draggingBlock = blockObj.GetComponent<BlockManager>();
    draggingBlock.Block = draggingBlock.Block.Clone();
    draggingBlock.Block.Id = Generator.uuid();
  }

  // Event trigger
  public void OnDragging(BaseEventData eventData) {
    if (draggingBlock is null) {
      return;
    }

    BlockSpawner.DrawBlockOnMousePos(draggingBlock.transform, true);
  }

  // Event trigger
  public void OnEndDrag(BaseEventData eventData) {
    containerImg.color = defaultColor;

    if (draggingBlock is null) {
      return;
    }

    draggingBlock.gameObject.tag = "Block";
    draggingBlock.gameObject.layer = LayerMask.NameToLayer("Map");

    // TODO
    PlayerBlockItemModel blocks = playerData.Data.Inventory.Blocks.Find(b => b.Type == Type);
    if (blocks == null) {
      return;
    }
    blocks.Qty--;
    observer.NotifyObservers(new PlayerDataSignal(playerData.Data));

    mapData.Data.Blocks.Add(new MapBlockModel(
      draggingBlock.Block,
      draggingBlock.transform.position,
      draggingBlock.transform.rotation
    ));
    observer.NotifyObservers(new MapDataSignal(mapData.Data));
  }
}
