using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeekNearest : BasicCode {

    public TagMasterSO tagmasterso;

    private void Start()
    {
        tagmasterso.Initialize();
    }

    public override void Run() {
        List<GameObject> enemies = new List<GameObject>();
        for (int i = 0; i < tagmasterso.Tags.Count; i++)
        {
            string currTag = tagmasterso.Tags[i];
            GameObject[] temp = GameObject.FindGameObjectsWithTag(currTag);
            for (int j = 0; j < temp.Length; j++)
            {
                enemies.Add(temp[j]);
            }
        }
        float minDist = Mathf.Infinity;
        Transform target = null;
        for (int i = 0; i < enemies.Count; i++) {
            float dist = Vector3.Distance(enemies[i].transform.position, towerRef.position);
            if (dist < minDist) {
                minDist = dist;
                target = enemies[i].transform;
            }
        }
        towerRef.GetComponent<TurretShooting>().SetTarget(target, minDist);
    }
}
