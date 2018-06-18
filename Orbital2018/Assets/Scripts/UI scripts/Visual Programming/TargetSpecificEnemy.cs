using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TargetSpecificEnemy : BasicCode {

    public Dropdown dropdown;

    private string enemyTag;
    public TagMasterSO tagmasterso;

    List<string> enemies = new List<string>() { "Please Select Enemy", "Enemy", "Boss" };

    public void DropdownIndexChanged(int index)
    {
        enemyTag = enemies[index];
    }

    private void Start()
    {
        PopulateList();
    }

    public override void Run()
    {
      
        if (enemyTag == null || enemyTag == "Please Select Enemy")
            return;

        TSE(enemyTag);
    }

    public void PopulateList()
    {
        dropdown.AddOptions(enemies);
    }

    void TSE (string enemyTag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
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
