using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private bool isActive = true;

	public void OnPointerEnter(PointerEventData data) {
        if (data.pointerDrag == null || !isActive) return;
        Drag d = data.pointerDrag.gameObject.GetComponent<Drag>();
        if (d != null) {
            d.dummyParent = transform;
        }
    }

	public void OnPointerExit(PointerEventData data) {
        if (data.pointerDrag == null || !isActive) return;
        Drag d = data.pointerDrag.gameObject.GetComponent<Drag>();
        if (d != null) {
            d.dummyParent = null;
            d.dummyIndex = -1;
        }
    }

    public void DisableDropZone()
    {
        isActive = false;
    }

    public void EnableDropZone()
    {
        isActive = true;
    }
}
