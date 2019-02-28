using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager bm;

    public static bool isDuplicated;
    public static TurretBlueprint duplicatedBlueprint;
    public static GameObject duplicatedPrefab;
    public static GameObject duplicatedTowerconsole;
    public static GameObject duplicatedCodeshop;

    [SerializeField]
    private TurretBlueprint turretToBuild;

    void Awake() {
        if (bm != null) {
            Debug.Log("More than 1 build manager in scene");
        }
        else bm = this;
        isDuplicated = false;
    }
   
    public TurretBlueprint GetTurretToBuild() {
        return turretToBuild;
    }

    public void SetTurretToBuild(TurretBlueprint turret) {
        turretToBuild = turret;
        if (InterfaceManager.ui.selectedNode == null) return;
        InterfaceManager.ui.selectedNode.ResetSelection();
        InterfaceManager.ui.selectedNode = null;
        return;
    }

    public void ResetTurretToBuild()
    {
        turretToBuild = null;
    }

    public void ResetDupTurret()
    {
        isDuplicated = false;
        duplicatedBlueprint = null;
        duplicatedPrefab = null;
        duplicatedTowerconsole = null;
        duplicatedCodeshop = null;
}
}
