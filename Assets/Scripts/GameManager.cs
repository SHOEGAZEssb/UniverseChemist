using Assets.Scripts.SaveLoad;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;

/// <summary>
/// Management object for a <see cref="Game"/>.
/// </summary>
public class GameManager : MonoBehaviour
{
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
    UpdateEntryList();

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
      UpdateEntryList();
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

  #region Save / Load

  /// <summary>
  /// Saves the game to file.
  /// </summary>
  public void SaveGame()
  {
    var chemicals = FindObjectsOfType<ChemicalBehaviour>().Select(c => new ChemicalBehaviourData(c.Chemical.Name, c.transform.position)).ToList();
    var saveData = new FullSaveData(chemicals, GameSaveData.FromGame(_game));

    var serializer = new DataContractSerializer(typeof(FullSaveData));
    using (var w = XmlWriter.Create("game.xml", new XmlWriterSettings { Indent = true }))
      serializer.WriteObject(w, saveData);
  }

  /// <summary>
  /// Loads the game from file.
  /// </summary>
  public void LoadGame()
  {
    FullSaveData saveData;
    var serializer = new DataContractSerializer(typeof(FullSaveData));
    using (var fs = new FileStream("game.xml", FileMode.Open))
    {
      saveData = (FullSaveData)serializer.ReadObject(fs);
    }

    ClearWorkspace();
    _game = Game.FromGameData(saveData.GameSaveData);
    foreach (var ch in saveData.ActiveChemicalBehaviours)
      InstantiateChemical(new Chemical(ch.ChemicalName), ch.Position);

    UpdateEntryList();
  }

  #endregion Save / Load

  public void ClearWorkspace()
  {
    _game.CleanUp();
    var ac = FindObjectsOfType<ChemicalBehaviour>();
    foreach (var c in ac)
      Destroy(c.gameObject);
  }

  private void InstantiateChemicals(IEnumerable<IChemical> chemicals, Vector2 position)
  {
    foreach (var newChemical in chemicals)
    {
      InstantiateChemical(newChemical, position);
    }
  }

  private void InstantiateChemical(IChemical chemical, Vector2 position)
  {
    var obj = Instantiate(Resources.Load<GameObject>("Prefabs/Chemical"), position, Quaternion.identity);
    obj.GetComponent<ChemicalBehaviour>().Chemical = chemical;
  }

  private void UpdateEntryList()
  {
    _entryFill.UpdateEntries(_game.UnlockedChemicals);
  }
}