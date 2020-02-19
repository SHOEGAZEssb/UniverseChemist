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
    _game.ActiveChemicalsChanged += Game_ActiveChemicalsChanged;

    _entryFill = FindObjectOfType<ChemicalEntryListFill>();
    _entryFill.UpdateEntries(_game.UnlockedChemicals);

    InstantiateChemicals(_game.ActiveChemicals, Vector3.zero);
  }

  public void Combine(IEnumerable<IChemical> chemicals)
  {
    _game.Combine(chemicals);
  }

  public IChemical AddChemicalToWorkspace(string name)
  {
    _game.AddChemicalToWorkspace(name);
    return _game.ActiveChemicals.Last();
  }

  private void Game_ActiveChemicalsChanged(object sender, ActiveChemicalsChangedEventArgs e)
  {
    var active = FindObjectsOfType<ChemicalBehaviour>();

    // destroy old chemicals
    Vector3? firstRemovedPosition = null;
    foreach (var removedChemical in e.RemovedChemicals)
    {
      var toDestroy = active.First(c => c.Chemical == removedChemical).gameObject;
      if(!firstRemovedPosition.HasValue)
        firstRemovedPosition = toDestroy.transform.position;
      Destroy(toDestroy);
    }

    // create new chemicals
    InstantiateChemicals(e.NewChemicals, firstRemovedPosition ?? Vector3.zero);
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