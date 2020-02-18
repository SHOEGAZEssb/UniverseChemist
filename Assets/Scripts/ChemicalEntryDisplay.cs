using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChemicalEntryDisplay : MonoBehaviour
{
  public Text NameText;
  public Image Sprite;

  public IChemical Chemical
  {
    get => _chemical;
    set
    {
      _chemical = value;
      NameText.text = Chemical.Name;
      Sprite.sprite = SpriteResolver.ResolveChemical(Chemical.Name);
    }
  }
  private IChemical _chemical;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
