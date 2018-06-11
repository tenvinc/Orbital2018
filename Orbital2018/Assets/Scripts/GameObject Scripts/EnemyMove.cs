using UnityEngine;

public class EnemyMove : MonoBehaviour {

    [Header("Enemy range threshold")]
    public float threshold = 0.4f;

    private Enemy enemy;

    // Make public for debugging
    private Transform target;
    public int nextNodeIndex;

    void Start() {
        enemy = GetComponent<Enemy>();
        nextNodeIndex = 0;
        target = Waypoints.waypoints[nextNodeIndex];
    }

    void Update() {
        Vector3 dir = target.position - transform.position;
        float dist = Vector3.Distance(target.position, transform.position);
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime);
        if (dist <= threshold) {
            getNextNode();
        }
    }

    void getNextNode() {
        if (nextNodeIndex >= Waypoints.waypoints.Length - 1) {
            Debug.Log("reached the end");
            Destroy(this.gameObject);
            return;
        }
        nextNodeIndex++;
        target = Waypoints.waypoints[nextNodeIndex];
    }


}
