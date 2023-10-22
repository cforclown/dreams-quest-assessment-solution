using BlockSystem;
using PlayerSystem;
using UnityEngine;

public static class MockServer {
  private static readonly string PLAYER_DATA_KEY = "player-data";
  private static readonly string MAP_DATA_KEY = "map-data";

  public static PlayerModel LoadPlayerData() {
    string playerDataStr = PlayerPrefs.GetString(PLAYER_DATA_KEY, null);
    if (playerDataStr is null || playerDataStr == "") {
      PlayerModel data = PlayerModel.Create("");
      SaveData(PLAYER_DATA_KEY, data);
      return data;
    }

    return JsonUtility.FromJson<PlayerModel>(playerDataStr);
  }

  public static void SavePlayerData(PlayerModel data) {
    SaveData(PLAYER_DATA_KEY, data);
  }

  public static MapModel LoadMapData() {
    string mapDataStr = PlayerPrefs.GetString(MAP_DATA_KEY, null);
    if (mapDataStr is null || mapDataStr == "") {
      MapModel data = MapModel.Create("Map1");
      SaveData(MAP_DATA_KEY, data);
      return data;
    }

    return JsonUtility.FromJson<MapModel>(mapDataStr);
  }

  public static void SaveMapData(MapModel data) {
    SaveData(MAP_DATA_KEY, data);
  }

  public static void SaveData<T>(string key, T data) {
    PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
  }
}
