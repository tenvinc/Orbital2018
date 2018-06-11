using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public void OnPointerEnter(PointerEventData data) {
        if (data.pointerDrag == null) return;
        Drag d = data.pointerDrag.gameObject.GetComponent<Drag>();
        if (d != null) {
            d.dummyParent = transform;
        }
        Debug.Log("Entering zone");
    }

	public void OnPointerExit(PointerEventData data) {
        if (data.pointerDrag == null) return;
        Drag d = data.pointerDrag.gameObject.GetComponent<Drag>();
        if (d != null) {
            d.dummyParent = null;
            d.dummyIndex = -1;
        }
        Debug.Log("Exiting zone");
    }

	 public void OnDrop(PointerEventData data) {
        Debug.Log("Dropping on " + gameObject.name);
    }
}
