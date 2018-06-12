using UnityEngine;

public class TargetSpecificEnemy : BasicCode {

    public string enemyTag;

    public override void Run()
    {
        TSE(enemyTag);
    }

    void TSE (string enemyTag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TagManager.tm.enemyTag);
        float minDist = Mathf.Infinity;
        Transform target = null;
        for (int i = 0; i < enemies.Length - 1; i++)
        {
            float dist = Vector3.Distance(enemies[i].transform.position, towerRef.position);
            if (dist < minDist)
            {
                minDist = dist;
                target = enemies[i].transform;
            }
        }
        towerRef.GetComponent<TurretShooting>().SetTarget(target, minDist);
    }
}
