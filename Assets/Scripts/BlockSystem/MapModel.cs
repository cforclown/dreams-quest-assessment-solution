using System;
using System.Collections.Generic;
using UnityEngine;


namespace BlockSystem {
  [Serializable]
  public class MapBlockPositionModel {
    public float x;
    public float y;
    public float z;

    public MapBlockPositionModel(Vector3 pos) {
      x = pos.x; y = pos.y; z = pos.z;
    }

    public Vector3 ToVector3() {
      return new Vector3(x, y, z);
    }
  }

  [Serializable]
  public class MapBlockRotationModel {
    public float x;
    public float y;
    public float z;

    public MapBlockRotationModel(Quaternion rot) {
      x = rot.eulerAngles.x;
      y = rot.eulerAngles.y;
      z = rot.eulerAngles.z;
    }

    public Quaternion ToQuaternion() {
      return Quaternion.Euler(x, y, z);
    }
  }

  [Serializable]
  public class MapBlockModel {
    public Block Block;
    public MapBlockPositionModel Position;
    public MapBlockRotationModel Rotation;

    public MapBlockModel(Block block, Vector3 pos, Quaternion rot) {
      Block = block;
      Position = new MapBlockPositionModel(pos);
      Rotation = new MapBlockRotationModel(rot);
    }

    public static MapBlockModel FromGameObject(Block block, GameObject gameObject) {

      return new MapBlockModel(
        block,
        gameObject.transform.position,
        gameObject.transform.rotation
      );
    }
  }

  [Serializable]
  public class MapModel {
    public string MapName;
    public List<MapBlockModel> Blocks;

    public MapModel(string mapName, List<MapBlockModel> blocks) {
      this.MapName = mapName;
      this.Blocks = blocks;
    }

    public static MapModel Create(string mapName) {
      return new MapModel(mapName, new List<MapBlockModel>());
    }
  }
}
