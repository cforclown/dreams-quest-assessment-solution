using PlayerSystem;

public struct SavePlayerDataSignal {
  public PlayerModel Data;

  public SavePlayerDataSignal(PlayerModel data) { this.Data = data; }
}
