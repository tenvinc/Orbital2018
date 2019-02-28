using UnityEngine;

public class DisplayConsole : MonoBehaviour {

    public GameObject towerPrefab;

    // Change to public for debugging
    private Transform visibleParent;
    private Transform hiddenParent;
    private Transform visibleCSParent;
    public Transform towerConsole = null;
    [HideInInspector]public Transform codeShop = null;

    private bool activated;

    void Start() {
        visibleParent = InterfaceManager.ui.visibleUI;
        hiddenParent = InterfaceManager.ui.hiddenUI;
        visibleCSParent = InterfaceManager.ui.visibleCSUI;
        activated = false;
    }

    void Update()
    {
        if (towerConsole == null) return;
        if (this.GetComponent<TurretNode>().isSelected)
        {
            if (!activated)
            {
                ShowCodeShop();
                ShowConsole();
                activated = true;
            }
        }
        else
        {
            if (activated)
            {
                if (codeShop.parent == visibleCSParent && InterfaceManager.ui.activeCSUser == transform)
                    HideCodeShop();
                HideConsole();
                activated = false;
            }
            else if (codeShop.parent == visibleCSParent && InterfaceManager.ui.activeCSUser == transform)
                HideCodeShop();
        }
    }

    public void CreateConsole() {
        GameObject instance = (GameObject)Instantiate(towerPrefab);
        towerConsole = instance.transform;
        ShowConsole();
    }

	public Transform GetTowerConsoleRef() {
		return towerConsole;
	}

    void OnMouseDown() {
        if (towerConsole == null && GetComponent<TurretNode>().turret != null) {
            CreateConsole();
            activated = true;
            codeShop = InterfaceManager.ui.GetValue(GetComponent<TurretNode>().turret.tag);
            ShowCodeShop();
            return;
        }
    }

    void ShowCodeShop()
    {
        InterfaceManager.ui.activeCSUser = transform;
        codeShop.SetParent(visibleCSParent, false);
    }

    void ShowConsole()
    {
        towerConsole.SetParent(visibleParent, false);
    }

    void HideCodeShop()
    {
        codeShop.SetParent(hiddenParent, false);
    }

    void HideConsole()
    {
        towerConsole.transform.SetParent(hiddenParent, false);
    }
}
