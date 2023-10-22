using Newtonsoft.Json;
using System;
using UnityEngine;

namespace BlockSystem {
  public interface IBlock : ICloneable {
    public string AssetId { get; }
    public string Id { get; }
    public string Name { get; }
    public BlockTypes Type { get; }
  }

  [CreateAssetMenu(fileName = "Block", menuName = "ScriptableObjects/Block")]
  public class Block : ScriptableObject, IBlock {
    [JsonProperty]
    [field: SerializeField]
    public string AssetId { get; private set; }

    [JsonProperty]
    public string Id { get; set; }

    [JsonProperty]
    [field: SerializeField]
    public string Name { get; private set; }

    [JsonProperty]
    [field: SerializeField]
    public BlockTypes Type { get; private set; }

    public Block Clone() => ((IBlock)this).Clone() as Block;

    object System.ICloneable.Clone() => (object)this.MemberwiseClone();
  }
}
