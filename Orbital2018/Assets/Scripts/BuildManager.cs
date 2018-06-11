using UnityEngine;

public class BuildManager : MonoBehaviour {

    // TODO: Change this to arrays when there are more than 1 prefab
    public GameObject prefab;
    public static BuildManager bm;

    [SerializeField]
    private TurretBlueprint turretToBuild;

    void Awake() {
        if (bm != null) {
            Debug.Log("More than 1 build manager in scene");
        }
        else bm = this;
    }
   
    public TurretBlueprint GetTurretToBuild() {
        return turretToBuild;
    }

    public void SetTurretToBuild(TurretBlueprint turret) {
        turretToBuild = turret;
        return;
    }
}
