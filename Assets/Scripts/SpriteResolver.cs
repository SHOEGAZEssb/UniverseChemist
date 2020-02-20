using System.Collections.Generic;
using UnityEngine;

static class SpriteResolver
{
  public static Sprite ResolveChemical(string name)
  {
    try
    {
      var sprite = Resources.Load<Sprite>($"Sprites/Chemicals/{name}");
      return sprite == null ? Resources.Load<Sprite>($"Sprites/Chemicals/Placeholder2") : sprite;
    }
    catch
    {
      return Resources.Load<Sprite>($"Sprites/Chemicals/Placeholder2");
    }
  }
}