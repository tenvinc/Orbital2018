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

    public bool isSelected;
    public TurretBlueprint blueprint;

    void Start() {
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.sharedMaterial;
        isSelected = false;
    }

	void OnMouseEnter() {
        rend.sharedMaterial = litMaterial;
    }

    void OnMouseExit() {
        if (isSelected) return;
        rend.sharedMaterial = defaultMaterial;
    }

    void OnMouseDown() {
        if (InterfaceManager.ui.isUIOverride) return;
        InterfaceManager.ui.towerMenu.SetActive(false);
        rend.sharedMaterial = clickedMaterial;
        UpdateSelected();
        if (turret != null) {
            //Debug.Log("Turret in place. Cannot build here.");
            turret.GetComponent<TurretShooting>().ShowRange();
            BuildManager.bm.ResetTurretToBuild();
            BuildManager.bm.ResetDupTurret();
        }
        else if (BuildManager.isDuplicated)
        {
            DuplicateTurret();
        }
        else {
            BuildTurret(BuildManager.bm.GetTurretToBuild());
        }
    }

    void DuplicateTurret ()
    {
        if (BuildManager.isDuplicated == false)
        {
            Debug.Log("Select the duplicate option!");
            return;
        }
        if (PlayerStats.Money < BuildManager.duplicatedBlueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= BuildManager.duplicatedBlueprint.cost;

        turret = (GameObject)Instantiate(BuildManager.duplicatedPrefab, transform.position, transform.rotation);
        turret.transform.SetParent(transform);
        // To make sure that turret instantiates correctly
        turret.transform.localPosition = Vector3.zero;
        turret.transform.localScale = Vector3.one;
        blueprint = BuildManager.duplicatedBlueprint;


        this.GetComponent<DisplayConsole>().towerConsole = ((GameObject)Instantiate(BuildManager.duplicatedTowerconsole)).transform;
        this.GetComponent<DisplayConsole>().codeShop = ((GameObject)Instantiate(BuildManager.duplicatedCodeshop)).transform;
        BasicCode towerConsoleRef = this.GetComponent<DisplayConsole>().towerConsole.transform.GetChild(1).GetComponent<BasicCode>();
        towerConsoleRef.SetTowerRef(turret.transform);
        towerConsoleRef.UpdateTowerRef();
    }

    void BuildTurret (TurretBlueprint _blueprint)
    {
        if (_blueprint == null)
        {
            //Debug.Log("Please select a turret.");
            return;
        }
        if (PlayerStats.Money < _blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= _blueprint.cost;

        GameObject selectedPrefab = _blueprint.prefab;
       
        turret = (GameObject)Instantiate(selectedPrefab, transform.position, transform.rotation);
        turret.transform.SetParent(transform);
        // To make sure that turret instantiates correctly
        turret.transform.localPosition = Vector3.zero;
        turret.transform.localScale = Vector3.one;
        blueprint = _blueprint;
    }

    public void ResetSelection ()
    {
        isSelected = false;
        rend.sharedMaterial = defaultMaterial;
        if (turret != null)
            turret.GetComponent<TurretShooting>().HideRange();
    }

    void UpdateSelected()
    {
        isSelected = true;
        InterfaceManager.ui.clickSourceKnown = true;
        // Reset previous selection
        if (InterfaceManager.ui.selectedNode != null && InterfaceManager.ui.selectedNode != this)
            InterfaceManager.ui.selectedNode.ResetSelection();
        InterfaceManager.ui.selectedNode = this;
    }

    public void SellTurret()
    {
        PlayerStats.Money += blueprint.cost;
        blueprint = null;
        Destroy(this.GetComponent<DisplayConsole>().towerConsole.gameObject);
        Destroy(turret.gameObject);
        ResetSelection();
    }
}
