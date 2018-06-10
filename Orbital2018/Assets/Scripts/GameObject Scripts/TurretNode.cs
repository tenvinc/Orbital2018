using UnityEngine;

public class TurretNode : MonoBehaviour {

    [Header("UI settings")]
    public Color litColor;
    public Color clickedColor;

    // TODO: Can be changed to private 
    [Header("Active turret")]
    public GameObject turret = null;

    // Stores original information
    private Renderer rend;
    private Color defaultColor;

    void Start() {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
    }

	void OnMouseEnter() {
        rend.material.color = litColor;
    }

    void OnMouseExit() {
        rend.material.color = defaultColor;
    }

    void OnMouseDown() {
        rend.material.color = clickedColor;
        if (turret != null) {
            Debug.Log("Turret in place. Cannot build here.");
            // TODO: Can show the script corresponding to the current turret on UI 
        }
        else {
            Debug.Log("Choose a turret");
            GameObject selectedPrefab = BuildManager.bm.GetTurretToBuild();
            if (selectedPrefab == null) {
                Debug.Log("Please select a turret.");
                return;
            }
            turret = (GameObject) Instantiate(selectedPrefab, transform.position, transform.rotation);
            turret.transform.SetParent(transform);
            // To make sure that turret instantiates correctly
            turret.transform.localPosition = Vector3.zero;
            turret.transform.localScale = Vector3.one;
        }
    }
}
