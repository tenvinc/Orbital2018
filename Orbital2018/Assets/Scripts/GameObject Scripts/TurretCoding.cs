using UnityEngine;

public class TurretCoding : MonoBehaviour {

    public float timeBetweenActions = 0.5f;
    public string enemyTag = "Enemy";

	void Start () {
        InvokeRepeating("SeekNearest", 0.5f, timeBetweenActions);
	}
	// TODO: Move this into a separate script for Visual Programming
	void SeekNearest() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float minDist = Mathf.Infinity;
        Transform target = null;
        for (int i=0; i<enemies.Length-1; i++) {
            float dist = Vector3.Distance(enemies[i].transform.position, transform.position);
            if (dist < minDist) {
                minDist = dist;
                target = enemies[i].transform;
            }
        }
        GetComponent<TurretShooting>().SetTarget(target, minDist);
    }
}
