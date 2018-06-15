using UnityEngine;

public class MortarShooting : TurretShooting {

    private float gravityConst;
    public Transform refPoint;

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
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector3 projectileInXZ = new Vector3(projectile.transform.position.x, 0f, projectile.transform.position.z);
        Vector3 targetInXZ = new Vector3(target.transform.position.x, 0f, target.transform.position.z);
        Projectile projectilePhysics = projectile.GetComponent<Projectile>();

        projectile.transform.LookAt(targetInXZ);

        // shorthand for formula https://vilbeyli.github.io/Projectile-Motion-Tutorial-for-Arrows-and-Missiles-in-Unity3D/#targetlocations
        float R = Vector3.Distance(projectileInXZ, targetInXZ);
        //Debug.Log("R is " + R);
        float G = -projectilePhysics.gravity;
        //Debug.Log("G is " + G);
        Vector3 firePointXZ = new Vector3 (firePoint.position.x, 0f, firePoint.position.z);
        Vector3 refPointXZ = new Vector3(refPoint.position.x, 0f, refPoint.position.z);
        float tanAlpha = (firePoint.position.y - refPoint.position.y) / Vector3.Distance(firePointXZ, refPointXZ);
       // Debug.Log("tan alpha is " + tanAlpha);
        float H = target.position.y - projectile.transform.position.y;
        //Debug.Log("H is " + H);

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
       // Debug.Log("Vz is " + Vz);
        float Vy = tanAlpha * Vz;
       // Debug.Log("Vy is " + Vy);

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = projectile.transform.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        //Debug.Log(globalVelocity.x);
        projectilePhysics.Initialise(globalVelocity);
    }
}
