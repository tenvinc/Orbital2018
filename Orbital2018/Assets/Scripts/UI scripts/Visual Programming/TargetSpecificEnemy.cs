using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class TargetSpecificEnemy : BasicCode {

    public TMP_Dropdown dropdown;

    private string enemyTag;

    private void Start()
    {
        PopulateList();
    }

    public override void Run()
    {
        enemyTag = LevelManager.level.spawner.enemies[dropdown.value];
        if (enemyTag == null || enemyTag == "Please Select Enemy")
            return;

        TSE(enemyTag);
    }

    public void PopulateList()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(LevelManager.level.spawner.enemies);
    }

    void TSE (string enemyTag)
    {
        if (towerRef == null) return;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float minDist = Mathf.Infinity;
        Transform target = null;
        for (int i = 0; i < enemies.Length; i++)
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
