using UnityEngine;

public class SeekNearest : BasicCode {

    public Transform tower;
    public string enemyTag = "Enemy";

    public override void Run() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float minDist = Mathf.Infinity;
        Transform target = null;
        for (int i = 0; i < enemies.Length - 1; i++) {
            float dist = Vector3.Distance(enemies[i].transform.position, tower.position);
            if (dist < minDist) {
                minDist = dist;
                target = enemies[i].transform;
            }
        }
        tower.GetComponent<TurretShooting>().SetTarget(target, minDist);
    }
}
