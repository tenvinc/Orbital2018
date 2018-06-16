using UnityEngine;

[CreateAssetMenu(fileName = "Regular", menuName = "Turret/Regular", order = 1)]
public class Regular : Turret
{
    public GameObject bulletPrefab;
    public override void Shoot(TurretShooting turret, Transform _target)
    {
        Transform firePoint = turret.firePoint;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().SetTarget(_target);
    }
}
