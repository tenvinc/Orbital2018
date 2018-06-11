using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public Transform originalParent = null;
	public Transform dummyParent = null;

	public GameObject dummyPrefab;
	public GameObject myPrefab;

	private Transform dummy = null;
	private Transform clone = null;

	public int originalIndex;
    public int dummyIndex;
	public float threshold = 100f;
	public bool inConsole = false;

	public void OnBeginDrag(PointerEventData data) {
		originalParent = transform.parent;
		originalIndex = transform.GetSiblingIndex();
		//Debug.Log("Start dragging from " + originalParent.parent.name);
        transform.SetParent(originalParent.parent, false);
		if (!inConsole) {
			clone = Instantiate(myPrefab, transform.position, transform.rotation).transform;
			clone.SetParent(originalParent, false);
			clone.SetSiblingIndex(originalIndex);	
		}
        GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData data) {
        transform.position = data.position;
		if (dummyParent != null) {
			if (dummy == null) {
				dummy = Instantiate(dummyPrefab, transform.position, transform.rotation).transform;
				dummyIndex = -1;
			}
            dummy.transform.SetParent(dummyParent, false);
			// Find nearest match
			float minDist = Mathf.Infinity;
			int indexToSwap = dummyIndex;
			Transform transformToSwap = dummy.transform;
			foreach (Transform c in dummyParent) {
				float distDiff = Vector2.Distance(c.position, transform.position);
				if (distDiff < minDist) {
					minDist = distDiff;
					indexToSwap = c.GetSiblingIndex();
					transformToSwap = c;
				}
			}
			if (minDist > threshold) return;
			transformToSwap.SetSiblingIndex(dummy.GetSiblingIndex());
			dummy.transform.SetSiblingIndex(indexToSwap);
			if (dummyIndex == -1) 
				dummyIndex = dummyParent.childCount;
			else 
				dummyIndex = indexToSwap;
		}
		else if (dummyParent == null && dummy != null) {
			Debug.Log("Out of bound. Destroying dummy now");
			dummyIndex = -1;
			Destroy(dummy.gameObject);
		}
	}

	public void OnEndDrag(PointerEventData data) {
		if (dummyParent == null) {
			Debug.Log("Lost connection, destruction starting now");
			Destroy(gameObject);
			return;
		}
		else {
			Destroy(dummy.gameObject);
			transform.SetParent(dummyParent, false);
			transform.SetSiblingIndex(dummyIndex);
			GetComponent<CanvasGroup>().blocksRaycasts = true;
			inConsole = true;
		}
	}

	void InitializeCode() {
		
	}
}
