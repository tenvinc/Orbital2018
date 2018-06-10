using UnityEngine;

public class TurretShooting : MonoBehaviour {

    [Header("Firing attribute")]
    public float turretRange = 100f;
    public float fireRate = 2f;

    [Header("Turning Attribute")]
    public float turnSpeed = 10f;

    [Header("Turret Admin")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform pivotToRotate;

    [SerializeField]
    private Transform target;
    private float fireCD;

    public void SetTarget(Transform _target, float distanceAway) {
        if (_target == null) return;
        // Keep current target until it goes out of range
        if (target != null) {
            float currTargetDist = Vector3.Distance(target.position, transform.position);
            if (currTargetDist > turretRange) 
                target = null;
        }
        if (target == null && distanceAway < turretRange) {
            target = _target;
        }
    }

    void Update() {
        if (target == null) return;
        // Lock on target
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(pivotToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        pivotToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCD <= 0) {
            Shoot();
            fireCD = 1 / fireRate;
        }
        fireCD -= Time.deltaTime;
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().SetTarget(target);
    }
}
