using UnityEngine;

public class IsBossPresent : BasicCode {

    public TagMasterSO tagmasterso;

    public override bool RunCheck()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagmasterso.BossTag);
        if (enemies.Length == 0)
        {
            return false;
        } return true;

    }

}
