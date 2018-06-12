using UnityEngine;

public class IfBlockZone : BasicCode {

    public Transform ifConditionZone;
    public Transform ifExecutionZone;
    public bool isConditionTrue;
    private int conditionNum;

    void Start() {
        ifExecutionZone.gameObject.SetActive(false);
    }

    void Update () {
        GetChildCount();
        if (conditionNum != 0)
        {
            ifExecutionZone.gameObject.SetActive(true);
            Debug.Log("Condition is inputted enabling execution block now");
        }
        else
        {
            ifExecutionZone.gameObject.SetActive(false);
        }
	}

    public override void Run() {
        if (conditionNum == 0) return;
        isConditionTrue = ifConditionZone.GetComponent<BasicCode>().RunCheck();
        if (isConditionTrue) {
            Debug.Log("statement is true");
            ifExecutionZone.GetComponent<BasicCode>().Run();
        }
        else {
            Debug.Log("statement is false");
        }
    }

    public override void SetTowerRef(Transform reference) {
        towerRef = reference;
        foreach (Transform c in transform)
        {
            c.GetComponent<BasicCode>().SetTowerRef(reference);
        }
        Debug.Log(towerRef.name);
    }

    void GetChildCount() {
        int count = 0;
        foreach (Transform child in ifConditionZone)
        {
            if (child.tag != TagManager.tm.dummyTag)
            {
                count++;
            }
        }
        conditionNum = count - 1;
    }
}
