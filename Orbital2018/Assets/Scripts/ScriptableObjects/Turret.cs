using UnityEngine;

public abstract class Turret : ScriptableObject
{
    [Header("Firing attribute")]
    public float turretRange = 100f;
    public float fireRate = 2f;

    [Header("Turning Attribute")]
    public float turnSpeed = 10f;

    public abstract void Shoot(TurretShooting turret, Transform target);
}
