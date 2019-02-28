using UnityEngine;

public abstract class Turret : ScriptableObject
{
    [Header("Firing attribute")]
    public float turretRange = 100f;
    public float fireRate = 2f;
    public float scaleProportion = 0.2f;

    [Header("Proper range display")]
    public int lineVtxCount = 40;
    public float lineWidth = 0.2f;

    [Header("Turning Attribute")]
    public float turnSpeed = 10f;

    public abstract void Shoot(TurretShooting turret, Transform target);
    public abstract LineRenderer BuildRangeCircle(Transform turret);
}
