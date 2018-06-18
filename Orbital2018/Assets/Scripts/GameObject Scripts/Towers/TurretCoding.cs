using UnityEngine;

public class TurretCoding : MonoBehaviour {

    public float timeBetweenActions = 0.5f;

	public Transform towerConsole;

    void Start () {
		towerConsole = transform.parent.GetComponent<DisplayConsole>().GetTowerConsoleRef();
		Debug.Log(towerConsole.name);
		towerConsole.GetComponent<BasicCode>().SetTowerRef(transform);
        InvokeRepeating("RunPlayerCode", 0f, timeBetweenActions);
	}

	void RunPlayerCode() {
		towerConsole.GetComponent<BasicCode>().Run();
	}
}
