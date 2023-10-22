using BlockSystem;
using System;
using System.Collections.Generic;

namespace PlayerSystem {
  [Serializable]
  public class PlayerInventoryModel {
    public List<PlayerBlockItemModel> Blocks;
  }

  [Serializable]
  public class PlayerBlockItemModel {
    public BlockTypes Type;
    public int Qty;

    public PlayerBlockItemModel(BlockTypes type, int qty) {
      this.Type = type;
      this.Qty = qty;
    }
  }
}
