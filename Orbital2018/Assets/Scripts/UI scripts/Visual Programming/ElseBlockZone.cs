using UnityEngine;

public class ElseBlockZone : BasicCode {

    public Transform ElseExecutionZone;

    private void Start()
    {
        if (transform.parent.tag == tagmasterso.CodeShopTag)
        {
            ElseExecutionZone.GetComponent<DropZone>().DisableDropZone();
        }
    }

    public override void Run()
    {
        if (CheckForPreviousIf())
        {
            return;
        }
        else
        {
            ElseExecutionZone.GetComponent<BasicCode>().Run();
        }
    }

    public override void SetTowerRef(Transform reference)
    {
        if (transform.parent.tag != tagmasterso.CodeShopTag)
            ElseExecutionZone.GetComponent<DropZone>().EnableDropZone();
        towerRef = reference;
        ElseExecutionZone.GetComponent<BasicCode>().SetTowerRef(towerRef);
    }

    // This checks if the previous if statements is true.
    bool CheckForPreviousIf()
    {
        int currentIndex = transform.GetSiblingIndex();
        int index = currentIndex - 1;
        if (index < 0) return true; 
        Transform previousCode = transform.parent.GetChild(index);
        if (previousCode.tag != tagmasterso.ifTag) return true;
        IfBlockZone previousIf = previousCode.GetComponent<IfBlockZone>();
        if (previousIf.IsIfBlockTrue()) return true;
        else return false;
    }
}
