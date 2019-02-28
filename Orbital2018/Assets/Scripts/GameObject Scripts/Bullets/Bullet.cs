using UnityEngine;

public class Bullet : MonoBehaviour {

    [Header("Bullet Attributes")]
    public float speed = 80f;
    public float damage = 50f;

    private Transform target;

    public void SetTarget(Transform _target) {
        target = _target;
    }

    void Update() {
        if (target == null) {
            //Debug.Log("Lost target");
            // Destroy(gameObject);
            gameObject.SetActive(false);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distThisFrame && target) {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distThisFrame, Space.World);
    }

    void HitTarget() {
        Damage(target);
        // Destroy(gameObject);
        gameObject.SetActive(false);
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        e.TakeDamage(damage);
    }
}
