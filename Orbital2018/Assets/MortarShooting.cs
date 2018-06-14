using UnityEngine;

public class MortarShooting : TurretShooting {

    void Start()
    {
        InvokeRepeating("SeekNearest", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null) return;
        // Lock on target
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(pivotToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        pivotToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCD <= 0)
        {
            ShootMortar();
            fireCD = 1 / fireRate;
        }
        fireCD -= Time.deltaTime;
    }

    void SeekNearest()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TagManager.tm.enemyTag);
        float minDist = Mathf.Infinity;
        Transform _target = null;
        for (int i = 0; i < enemies.Length - 1; i++)
        {
            float dist = Vector3.Distance(enemies[i].transform.position, transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                _target = enemies[i].transform;
            }
        }
        if (_target == null) return;
        // Keep current target until it goes out of range
        if (target != null)
        {
            float currTargetDist = Vector3.Distance(target.position, transform.position);
            if (currTargetDist > turretRange)
                target = null;
        }
        if (target == null && minDist < turretRange)
        {
            target = _target;
        }
    }

    void ShootMortar()
    {
        Debug.Log("mortar shoots");
        GameObject cannonball = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
