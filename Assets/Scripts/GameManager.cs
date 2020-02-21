using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Management object for a <see cref="Game"/>.
/// </summary>
public class GameManager : MonoBehaviour
{
  #region Properties

  public IEnumerable<string> UnlockedElements => _game.UnlockedChemicals;

  #endregion Properties

  #region Member

  /// <summary>
  /// The current game.
  /// </summary>
  private Game _game;

  private ChemicalEntryListFill _entryFill;

  #endregion Member

  // Start is called before the first frame update
  void Start()
  {
    _game = Game.New();

    _entryFill = FindObjectOfType<ChemicalEntryListFill>();
    _entryFill.UpdateEntries(_game.UnlockedChemicals);

    InstantiateChemicals(_game.ActiveChemicals, Vector3.zero);
  }

  public void Combine(IEnumerable<IChemical> chemicals)
  {
    var newChemicals = _game.Combine(chemicals);
    if(newChemicals.Any())
    {
      var active = FindObjectsOfType<ChemicalBehaviour>();

      // destroy old chemicals
      Vector3? firstRemovedPosition = null;
      foreach (var removedChemical in chemicals)
      {
        var toDestroy = active.First(c => c.Chemical == removedChemical).gameObject;
        if (!firstRemovedPosition.HasValue)
          firstRemovedPosition = toDestroy.transform.position;
        Destroy(toDestroy);
      }

      // create new chemicals
      InstantiateChemicals(newChemicals, firstRemovedPosition ?? Vector3.zero);
    }
  }

  public IChemical AddChemicalToWorkspace(string name)
  {
    var newChemical = _game.AddChemicalToWorkspace(name);
    return newChemical;
  }

  public void Clone(ChemicalBehaviour chemical)
  {
    var newChemical = AddChemicalToWorkspace(chemical.Chemical.Name);
    InstantiateChemicals(new[] { newChemical }, chemical.transform.position + (Vector3)new Vector2(.1f, .1f));
  }

  public void SaveGame()
  {
    _game.Save("game.xml");
  }

  public void LoadGame()
  {
    var newGame = Game.FromSaveFile("game.xml");
  }

  private void InstantiateChemicals(IEnumerable<IChemical> chemicals, Vector3 position)
  {
    foreach (var newChemical in chemicals)
    {
      var obj = Instantiate(Resources.Load<GameObject>("Prefabs/Chemical"), position, Quaternion.identity);
      obj.GetComponent<ChemicalBehaviour>().Chemical = newChemical;
    }
  }
}