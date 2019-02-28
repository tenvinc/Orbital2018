using UnityEngine;

public class IsBossPresent : BasicCode {

    public override bool RunCheck()
    {
        Turret turretSO = towerRef.GetComponent<TurretShooting>().turret;
        GameObject[] bossesInScene = GameObject.FindGameObjectsWithTag(tagmasterso.BossTag);
        Debug.Log(bossesInScene.Length);
        for (int i=0; i<bossesInScene.Length; i++)
        {
            if (Vector3.Distance(bossesInScene[i].transform.position, towerRef.transform.position) < turretSO.turretRange* turretSO.scaleProportion)
            {
                return true;
            }
        }
        return false;
    }
}
