using UnityEngine;

public class EnemyMove : MonoBehaviour {

    [Header("Enemy range threshold")]
    public float threshold = 0.4f;
    public float turnSpeed = 10f;

    private Enemy enemy;

    // Make public for debugging
    private Transform target;
    [HideInInspector]public int nextNodeIndex = 0;

    void Start() {
        enemy = GetComponent<Enemy>();
        target = Waypoints.waypoints[nextNodeIndex];       
    }

    void Update() {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRot, turnSpeed * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        float dist = Vector3.Distance(target.position, transform.position);
        transform.position += dir.normalized * enemy.speed * Time.deltaTime;
        if (dist <= threshold) {
            getNextNode();
        }
        
    }

    void getNextNode() {
        if (nextNodeIndex >= Waypoints.waypoints.Length - 1 && !this.GetComponent<Enemy>().isDead) {
            Instantiate(GetComponent<Enemy>().dieFX, transform.position, transform.rotation);
            Destroy(this.gameObject);
            LevelManager.level.currCount++;
            // TODO: Change this hardcode
            if (this.tag == "Boss")
            {
                PlayerStats.Lives -= 2;
                PlayerStats.livesLost += 2;
            }
            else
            {
                PlayerStats.Lives--;
                PlayerStats.livesLost++;
            }
            return;
        }
        nextNodeIndex++;
        target = Waypoints.waypoints[nextNodeIndex];
    }

    public void setNextNode(int index)
    {
        nextNodeIndex = index;
    }
}
