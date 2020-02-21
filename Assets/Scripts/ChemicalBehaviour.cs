using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Behaviour for representing an active <see cref="IChemical"/>.
/// </summary>
public class ChemicalBehaviour : MonoBehaviour
{
  #region Properties

  /// <summary>
  /// The chemical that this behaviour represents.
  /// </summary>
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

  #endregion Properties

  #region Member

  private GameManager _gameManager;
  private Collider2D _collider;
  private float _doubleClickStart = 0;

  #endregion Member

  private void Start()
  {
    _gameManager = FindObjectOfType<GameManager>();
    _collider = GetComponent<Collider2D>();
  }

  /// <summary>
  /// Combines this chemical with the nearest
  /// touching chemical.
  /// </summary>
  public void OnMouseUp()
  {
    // check for double click
    if ((Time.time - _doubleClickStart) < 0.2f)
    {
      OnDoubleClick();
      _doubleClickStart = -1;
      return;
    }
    else
      _doubleClickStart = Time.time;

    // check if chemical was dropped onto garbage
    var garbages = FindObjectsOfType<ChemicalGarbage>();
    foreach (var garbage in garbages)
    {
      if (_collider.bounds.Intersects(garbage.Collider.bounds))
      {
        Destroy(gameObject);
        return;
      }
    }

    var chemicals = FindObjectsOfType<ChemicalBehaviour>();
    foreach (var chemical in chemicals)
    {
      if (chemical != this && chemical.GetComponent<Collider2D>().bounds.Intersects(_collider.bounds))
      {
        _gameManager.Combine(new[] { Chemical, chemical.Chemical });
        return;
      }
    }
  }

  private void OnDoubleClick()
  {
    _gameManager.Clone(this);
  }
}
