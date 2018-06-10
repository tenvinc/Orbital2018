using UnityEngine;

public class TurretCoding : MonoBehaviour {

    public float timeBetweenActions = 0.5f;

    public Transform towerConsole;

	void Start () {
        InvokeRepeating("RunPlayerCode", 0.5f, timeBetweenActions);
        Transform parent = transform.parent;
        //towerConsole = parent.GetComponent<DisplayConsole>().GetConsoleRef();
        //if (towerConsole == null) {
           // Debug.Log("something is wrong please fix");
           // return;
       // }
        //towerConsole.GetComponent<BasicCode>().SetTowerRef(transform);
    }

    void RunPlayerCode() {
        towerConsole.GetComponent<BasicCode>().Run();
    }
}
