using UnityEngine;

public class TurretNode : MonoBehaviour {

    [Header("UI settings")]
    public Material litMaterial;
    public Material clickedMaterial;

    // TODO: Can be changed to private 
    [Header("Active turret")]
    public GameObject turret = null;

    // Stores original information
    private Renderer rend;
    private Material defaultMaterial;

    void Start() {
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.sharedMaterial;
    }

	void OnMouseEnter() {
        rend.sharedMaterial = litMaterial;
    }

    void OnMouseExit() {
        rend.sharedMaterial = defaultMaterial;
    }

    void OnMouseDown() {
        rend.sharedMaterial = clickedMaterial;
        if (turret != null) {
            Debug.Log("Turret in place. Cannot build here.");
            // TODO: Can show the script corresponding to the current turret on UI 
        }
        else {
            BuildTurret(BuildManager.bm.GetTurretToBuild());
        }
    }

    void BuildTurret (TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        Debug.Log("Choose a turret");
        GameObject selectedPrefab = blueprint.prefab;
        if (selectedPrefab == null)
        {
            Debug.Log("Please select a turret.");
            return;
        }
       
        turret = (GameObject)Instantiate(selectedPrefab, transform.position, transform.rotation);
        turret.transform.SetParent(transform);
        // To make sure that turret instantiates correctly
        turret.transform.localPosition = Vector3.zero;
        turret.transform.localScale = Vector3.one;

    }
}
