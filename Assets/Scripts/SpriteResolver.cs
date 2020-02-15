using System.Collections.Generic;
using UnityEngine;

static class SpriteResolver
{
  private static IDictionary<string, string> _chemicalSprites;

  static SpriteResolver()
  {
    _chemicalSprites = new Dictionary<string, string>();
  }

  public static Sprite ResolveChemical(string name)
  {
    if (_chemicalSprites.ContainsKey(name))
      return Resources.Load<Sprite>($"Sprites/Chemicals/{name}");
    else
      return Resources.Load<Sprite>($"Sprites/Chemicals/Placeholder2");
  }
}