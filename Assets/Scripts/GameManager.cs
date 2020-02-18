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

  public void Combine(IEnumerable<IChemical> chemicals)
  {
    _game.Combine(chemicals);
  }

  // Start is called before the first frame update
  void Start()
  {
    _game = Game.New();
    _game.ActiveChemicalsChanged += Game_ActiveChemicalsChanged;

    _entryFill = FindObjectOfType<ChemicalEntryListFill>();
    _entryFill.UpdateEntries(_game.UnlockedChemicals);

    InstantiateChemicals(_game.ActiveChemicals);
  }

  private void Game_ActiveChemicalsChanged(object sender, ActiveChemicalsChangedEventArgs e)
  {
    var active = FindObjectsOfType<ChemicalBehaviour>();

    // destroy old chemicals
    foreach (var removedChemical in e.RemovedChemicals)
      Destroy(active.First(c => c.Chemical == removedChemical).gameObject);

    // create new chemicals
    InstantiateChemicals(e.NewChemicals);
  }

  private void InstantiateChemicals(IEnumerable<IChemical> chemicals)
  {
    foreach (var newChemical in chemicals)
    {
      var obj = Instantiate(Resources.Load<GameObject>("Prefabs/Chemical"));
      obj.GetComponent<ChemicalBehaviour>().Chemical = newChemical;
    }
  }
}