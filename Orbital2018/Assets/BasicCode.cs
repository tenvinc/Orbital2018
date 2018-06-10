using UnityEngine;

public class BasicCode : MonoBehaviour {

    public Transform towerRef;

    public virtual void Run() {
        Debug.Log("By default this does nothing");
        foreach (Transform child in this.transform) {
            child.GetComponent<BasicCode>().Run();
        }
    }

    public void SetTowerRef(Transform reference) {
        towerRef = reference;
        Debug.Log(towerRef.name);
    }
}
