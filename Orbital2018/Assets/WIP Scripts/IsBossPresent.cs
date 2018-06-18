using UnityEngine;

public class IsBossPresent : BasicCode {

    public override bool RunCheck()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagmasterso.BossTag);
        if (enemies.Length == 0)
        {
            return false;
        } return true;

    }

}
