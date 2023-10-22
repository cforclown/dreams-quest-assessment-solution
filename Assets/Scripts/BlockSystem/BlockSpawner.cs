using GameEventSystem;
using GameSystem;
using PlayerSystem;
using System;
using UnityEngine;

namespace BlockSystem {
  public class BlockSpawner : MonoBehaviour {
    [SerializeField] public string MapName;

    public static string MapGameObjectName = "Map";

    private IObserver observer;
    private PlayerModelInstance playerData;
    private MapModelInstance mapData;

    private void Awake() {
      observer = GameStateManager.I?.Observer;
      playerData = GameStateManager.I?.Player;
      mapData = GameStateManager.I?.MapData;
    }

    private void Start() {
    }

    private void Update() {
      if ((Input.GetMouseButtonUp(1))) {
        CheckRightClickOnBlock();
      }
    }

    private void CheckRightClickOnBlock() {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Map"))) {
        BlockManager block = hit.transform.GetComponent<BlockManager>();
        if (block is null) {
          return;
        }

        RemoveBlockFromMap(block);
      }
    }

    private void RemoveBlockFromMap(BlockManager block) {
      PlayerBlockItemModel blockTypes = playerData.Data.Inventory.Blocks.Find(b => b.Type == block.Block.Type);
      if (blockTypes is null) {
        return;
      }

      blockTypes.Qty++;
      observer.NotifyObservers(new PlayerDataSignal(playerData.Data));

      mapData.Data.Blocks.RemoveAll(b => b.Block.Id == block.Block.Id);
      observer.NotifyObservers(new MapDataSignal(mapData.Data));

      GameObject.Destroy(block.gameObject);
    }

    public static void DrawBlockOnMousePos(Transform block, bool stackOnTop = true) {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Map"))) {
        if (hit.transform.gameObject.name != MapGameObjectName && hit.transform.gameObject.tag != "Block") {
          block.position = new Vector3(-1000f, -1000f, -1000f);
          return;
        }

        if (hit.transform.gameObject.tag == "Block") {
          if (stackOnTop) {
            block.position = hit.transform.position + Vector3.up;
          }

          return;
        }

        block.position = new Vector3(
          Mathf.RoundToInt(hit.point.x),
          Mathf.RoundToInt(hit.point.y) + 0.5f,
          Mathf.RoundToInt(hit.point.z)
        );
      }
    }

    public static Vector3? GetArenaPosByMousePos() {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Map"))) {
        if (hit.transform.gameObject.name != MapGameObjectName) {
          return null;
        }

        return new Vector3(
          Mathf.RoundToInt(hit.point.x),
          Mathf.RoundToInt(hit.point.y) + 0.5f,
          Mathf.RoundToInt(hit.point.z)
        );
      }

      return null;
    }

    public static Sprite LoadBlockSprite(BlockTypes type) {
      try {
        return Resources.Load<Sprite>(string.Format("Images/Blocks/{0}", GetBlockSpriteNameByType(type)));
      }
      catch (Exception e) {
        Debug.LogError(e.Message);
        return null;
      }
    }

    public static string GetBlockPrefabNameByType(BlockTypes type) {
      return type switch {
        BlockTypes.BlockType1 => "Block1.prefab",
        BlockTypes.BlockType2 => "Block2.prefab",
        BlockTypes.BlockType3 => "Block3.prefab",
        _ => ""
      };
    }

    public static string GetBlockSpriteNameByType(BlockTypes type) {
      return type switch {
        BlockTypes.BlockType1 => "Block1",
        BlockTypes.BlockType2 => "Block2",
        BlockTypes.BlockType3 => "Block3",
        _ => ""
      };
    }
  }
}
