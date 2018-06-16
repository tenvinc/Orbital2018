using UnityEngine;

[CreateAssetMenu(fileName = "Mortar", menuName = "Turret/Mortar", order = 2)]
public class Mortar : Turret
{
    public GameObject bulletPrefab;

    public override void Shoot(TurretShooting turret, Transform _target)
    {
        GameObject projectile = Instantiate(bulletPrefab, turret.firePoint.position, turret.firePoint.rotation);
        Projectile projectilePhysics = projectile.GetComponent<Projectile>();
        // the target is projected to be on the same y as projectile
        Vector3 targetInXZ = new Vector3(_target.position.x, projectile.transform.position.y, _target.position.z);
        Vector3 projectileInXZ = new Vector3(projectile.transform.position.x, projectile.transform.position.y, projectile.transform.position.z);

        projectile.transform.LookAt(targetInXZ);  

        // shorthand for formula https://vilbeyli.github.io/Projectile-Motion-Tutorial-for-Arrows-and-Missiles-in-Unity3D/#targetlocations
        float R = Vector3.Distance(projectileInXZ, targetInXZ);
        float G = -projectilePhysics._gravity;
        Vector3 firePointXZ = new Vector3(turret.firePoint.position.x, 0f, turret.firePoint.position.z);
        Vector3 refPointXZ = new Vector3(turret.refPoint.position.x, 0f, turret.refPoint.position.z);
        float tanAlpha = (turret.firePoint.position.y - turret.refPoint.position.y) / Vector3.Distance(firePointXZ, refPointXZ);
        float H = _target.position.y - projectile.transform.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = projectile.transform.TransformDirection(localVelocity);

        projectilePhysics.Initialise(globalVelocity);
    }
}
