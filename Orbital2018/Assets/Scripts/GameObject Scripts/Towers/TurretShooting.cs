using UnityEngine;

public class TurretShooting : MonoBehaviour {

    public Turret turret;
    public Transform firePoint;
    public Transform pivotToRotate;
    public Transform refPoint;

    [SerializeField]
    protected Transform target;
    protected float fireCD;

    public void SetTarget(Transform _target, float distanceAway) {
        if (_target == null) return;
        // Keep current target until it goes out of range
        if (target != null) {
            float currTargetDist = Vector3.Distance(target.position, transform.position);
            if (currTargetDist > turret.turretRange) 
                target = null;
        }
        if (target == null && distanceAway < turret.turretRange) {
            target = _target;
        }
    }

    void Update() {
        if (target == null) return;
        // Lock on target
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(pivotToRotate.rotation, lookRotation, turret.turnSpeed * Time.deltaTime).eulerAngles;
        pivotToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCD <= 0) {
            Shoot();
            fireCD = 1 / turret.fireRate;
        }
        fireCD -= Time.deltaTime;
    }

    void Shoot() {
        turret.Shoot(this, target);
    }
}
