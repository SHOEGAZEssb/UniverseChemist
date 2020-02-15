using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalBehaviour : MonoBehaviour
{
  public IChemical Chemical;

  private readonly List<ChemicalBehaviour> _touchedChemicals = new List<ChemicalBehaviour>();

  void OnTriggerEnter2D(Collider2D collider)
  {
    var chemical = collider.gameObject.GetComponent<ChemicalBehaviour>();
    if (chemical != null)
      _touchedChemicals.Add(chemical);
  }

  void OnTriggerExit2D(Collider2D collider)
  {
    var chemical = collider.gameObject.GetComponent<ChemicalBehaviour>();
    if (chemical != null)
      _touchedChemicals.Remove(chemical);
  }

  void OnMouseUp()
  {

  }

}
