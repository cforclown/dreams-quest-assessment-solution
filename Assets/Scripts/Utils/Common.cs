using System;

public static class Generator {
  public static string uuid() => Guid.NewGuid().ToString();

  public static int RandomInt(int min, int max) => UnityEngine.Random.Range(min, max);

  public static float RandomFloat(float min, float max) => UnityEngine.Random.Range(min, max);
}
