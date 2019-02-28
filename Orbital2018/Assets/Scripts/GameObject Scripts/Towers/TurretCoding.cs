using UnityEngine;

public class TurretCoding : MonoBehaviour {

    public float timeBetweenActions = 0.5f;

	private Transform towerConsole;

    void Start () {
		Transform towerConsoleGrp = transform.parent.GetComponent<DisplayConsole>().GetTowerConsoleRef();
        towerConsole = towerConsoleGrp.GetChild(1);
		//Debug.Log(towerConsole.name);
		towerConsole.GetComponent<BasicCode>().SetTowerRef(transform);
        InvokeRepeating("RunPlayerCode", 0f, timeBetweenActions);
	}

	void RunPlayerCode() {
		towerConsole.GetComponent<BasicCode>().Run();
	}
}
