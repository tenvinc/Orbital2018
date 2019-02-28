using UnityEngine;

public class IfBlockZone : BasicCode {

    public Transform ifConditionZone;
    public Transform ifExecutionZone;
    private int conditionNum;
    private bool isConditionTrue;

    void Start() {
        ifExecutionZone.gameObject.SetActive(false);
        isConditionTrue = false;
    }

    void Update () {
        if (transform.parent.tag == tagmasterso.CodeShopTag) return;
        GetChildCount();
        if (conditionNum != 0)
        {
            ifExecutionZone.gameObject.SetActive(true);
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
            ifExecutionZone.GetComponent<BasicCode>().Run();
        }
    }

    public override void SetTowerRef(Transform reference) {
        ifConditionZone.GetComponent<BasicCode>().SetTowerRef(reference);
        ifExecutionZone.GetComponent<BasicCode>().SetTowerRef(reference);

    }

    void GetChildCount() {
        int count = 0;
        foreach (Transform child in ifConditionZone)
        {
            if (child.tag != tagmasterso.DummyTag)
            {
                count++;
            }
        }
        conditionNum = count;
    }

    public bool IsIfBlockTrue()
    {
        return isConditionTrue;
    }
}
