using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 10f;

    private Transform target;

    public void SetTarget(Transform _target) {
        target = _target;
    }

    void Update() {
        if (target == null) {
            Debug.Log("Lost target");
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distThisFrame = bulletSpeed * Time.deltaTime;
        if (dir.magnitude <= distThisFrame) {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distThisFrame, Space.World);
    }

    void HitTarget() {
        Debug.Log("Hit");
        Destroy(gameObject);
    }
}
