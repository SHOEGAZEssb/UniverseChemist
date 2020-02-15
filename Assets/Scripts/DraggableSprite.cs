using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
  private Vector3 _offset;
  private SpriteRenderer _spriteRenderer;

  private void Start()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void OnMouseDown()
  {
    _spriteRenderer.sortingLayerName = "Dragging";
    _offset = transform.position - Camera.main.ScreenToWorldPoint(
      new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
  }

  private void OnMouseUp()
  {
    _spriteRenderer.sortingLayerName = "Default";
    transform.position = new Vector3(transform.position.x, transform.position.y, 0);
  }

  private void OnMouseDrag()
  {
    var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
    transform.position = curPosition;
  }
}
