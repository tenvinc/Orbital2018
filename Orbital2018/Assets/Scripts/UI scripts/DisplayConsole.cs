using UnityEngine;

public class DisplayConsole : MonoBehaviour {

    public GameObject towerConsolePrefab;

    // Change to public for debugging
    private Transform visibleParent;
    private Transform hiddenParent;
    private Transform towerConsole = null;

    void Start() {
        visibleParent = InterfaceManager.ui.visibleUI;
        hiddenParent = InterfaceManager.ui.hiddenUI;
    }

    public void CreateConsole() {
        GameObject instance = (GameObject)Instantiate(towerConsolePrefab);
        towerConsole = instance.transform;
        towerConsole.SetParent(visibleParent, false);
    }

    public Transform GetConsoleRef() {
        return towerConsole;
    }

    void OnMouseDown() {
        if (towerConsole == null && GetComponent<TurretNode>().turret != null) {
            // Debug.Log("Console not created");
            CreateConsole();
            return;
        }
        else if (towerConsole != null) {
            if (towerConsole.parent != visibleParent)
                towerConsole.SetParent(visibleParent, false);
            else
                towerConsole.transform.SetParent(hiddenParent, false);
        }
    }    
}
