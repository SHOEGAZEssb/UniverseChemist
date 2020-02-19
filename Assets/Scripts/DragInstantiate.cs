using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragInstantiate : MonoBehaviour
{
  public GameObject ChemicalPrefab;
  public ChemicalEntryDisplay EntryDisplay;

  private GameObject _instantiatedObject;
  private GameManager _gameManager;

  private bool _dragging;

  private void Start()
  {
    _gameManager = FindObjectOfType<GameManager>();
  }

  private void Update()
  {
    if (_dragging)
      _instantiatedObject.transform.position = GetMousePosition();
  }

  public void OnMouseDown()
  {
    var addedChemical = _gameManager.AddChemicalToWorkspace(EntryDisplay.Chemical.Name);
    _instantiatedObject = Instantiate(ChemicalPrefab, GetMousePosition(), Quaternion.identity);

    _instantiatedObject.GetComponent<ChemicalBehaviour>().Chemical = addedChemical;
    _dragging = true;
  }

  public void OnMouseUp()
  {
    _dragging = false;
    _instantiatedObject.GetComponent<ChemicalBehaviour>().OnMouseUp();
    _instantiatedObject = null;
  }

  private Vector3 GetMousePosition()
  {
    var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    return new Vector3(mousePos.x, mousePos.y, 0);
  }
}
