using BlockSystem;
using System;
using System.Collections.Generic;

namespace PlayerSystem {
  [Serializable]
  public class PlayerModel {
    public string Username;
    public PlayerInventoryModel Inventory;

    public static PlayerModel Create(string username) {
      return new PlayerModel() {
        Username = username,
        Inventory = new PlayerInventoryModel() {
          Blocks = new List<PlayerBlockItemModel>() {
            new PlayerBlockItemModel(BlockTypes.BlockType1, 100),
            new PlayerBlockItemModel(BlockTypes.BlockType2, 100),
            new PlayerBlockItemModel(BlockTypes.BlockType3, 100),
          }
        }
      };
    }
  }
}
