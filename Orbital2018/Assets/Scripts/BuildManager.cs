using UnityEngine;

public class BuildManager : MonoBehaviour {

    // TODO: Change this to arrays when there are more than 1 prefab
    public GameObject prefab;
    public static BuildManager bm;

    [SerializeField]
    private GameObject turretToBuild;

    void Awake() {
        if (bm != null) {
            Debug.Log("More than 1 build manager in scene");
        }
        else bm = this;
    }
   
    public GameObject GetTurretToBuild() {
        return turretToBuild;
    }

    public void SetTurretToBuild(string tag) {
        // Debug.Log("Trying to set turret to " + tag);
        if (prefab.tag == tag) {
            turretToBuild = prefab;
            // Debug.Log("Finished setting turret");
        }
        return;
    }
}
