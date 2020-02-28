using System.Collections.Generic;
using UnityEngine;

public class ChemicalEntryListFill : MonoBehaviour
{
  public GameObject EntryPrefab;

  public void UpdateEntries(IEnumerable<string> chemicals)
  {
    // remove old elements
    foreach (var entry in FindObjectsOfType<ChemicalEntryDisplay>())
      Destroy(entry.gameObject);

    foreach (var chemicalName in chemicals)
    {
      var entry = Instantiate(EntryPrefab);
      var entryDisplay = entry.GetComponent<ChemicalEntryDisplay>();
      entryDisplay.Chemical = new Chemical(chemicalName);
      entry.transform.SetParent(transform);
    }
  }
}