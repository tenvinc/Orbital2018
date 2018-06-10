using UnityEngine;

public class EnemyMove : MonoBehaviour {

    [Header("Enemy attributes")]
    public float speed = 10;
    public float threshold = 0.2f;

    // Make public for debugging
    private Transform target;
    public int nextNodeIndex;

    void Start() {
        nextNodeIndex = 0;
        target = Waypoints.waypoints[nextNodeIndex];
    }

    void Update() {
        Vector3 dir = target.position - transform.position;
        float dist = Vector3.Distance(target.position, transform.position);
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        if (dist < threshold) {
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
