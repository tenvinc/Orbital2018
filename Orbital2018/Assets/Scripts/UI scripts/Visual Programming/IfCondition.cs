using UnityEngine;

public class IfCondition : BasicCode {

    public override bool RunCheck() {
        for (int i=1; i<transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            if (child.GetComponent<BasicCode>().RunCheck())
                return true;
        }
        return false;
    }
}
