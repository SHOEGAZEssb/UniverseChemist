using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChemicalBehaviour : MonoBehaviour
{
  public IChemical Chemical
  {
    get => _chemical;
    set
    {
      _chemical = value;
      GetComponent<SpriteRenderer>().sprite = SpriteResolver.ResolveChemical(Chemical.Name);
    }
  }
  private IChemical _chemical;

  private GameManager _gameManager;
  private Collider2D _collider;

  private void Start()
  {
    _gameManager = FindObjectOfType<GameManager>();
    _collider = GetComponent<Collider2D>();
  }

  void OnMouseUp()
  {
    var chemicals = FindObjectsOfType<ChemicalBehaviour>();
    foreach(var chemical in chemicals)
    {
      if (chemical != this && chemical.GetComponent<Collider2D>().bounds.Intersects(_collider.bounds))
      {
        _gameManager.Combine(new[] { Chemical, chemical.Chemical });
        return;
      }
    }
  }
}
