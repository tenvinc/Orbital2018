using UnityEngine;

public class TurretShooting : MonoBehaviour {

    public Turret turret;
    public Transform firePoint;
    public Transform pivotToRotate;
    public Transform refPoint;
    public ParticleSystem debuffFX;
    public ParticleSystem muzzleFX;

    // Debuff variables
    private float debuffProportion;
    private float blindProportion;
    [HideInInspector] public bool isDebuffed;
    [HideInInspector] private bool isBlinded;

    public Transform target;
    protected float fireCD;
    protected float debuffCD;
    protected float blindedCD;

    private LineRenderer line;

    void Start()
    {
        line = turret.BuildRangeCircle(transform);
        debuffProportion = 1f;
        blindProportion = 1f;
        isDebuffed = false;
        isBlinded = false;
}

    public void SetTarget(Transform _target, float distanceAway) {
        if (_target == null) return;
        // Keep current target until it goes out of range
        float actualRange = turret.turretRange * turret.scaleProportion;
        if (target != null) {
            float currTargetDist = Vector3.Distance(target.position, transform.position);
            if (currTargetDist > actualRange) 
                target = null;
        }
        if (target == null && distanceAway < actualRange) {
            target = _target;
        }
    }

    void Update() {
        if (target != null)
        {
            // Lock on target
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(pivotToRotate.rotation, lookRotation, turret.turnSpeed * Time.deltaTime).eulerAngles;
            pivotToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        if (fireCD <= 0) {
            Shoot();
            fireCD = 1 / (turret.fireRate*debuffProportion);
        }
        fireCD -= Time.deltaTime;
        if (isDebuffed)
        {
            if (debuffCD <= 0)
            {
                resetDebuff();
            }
            debuffCD -= Time.deltaTime;
        }
        if (isBlinded)
        {
            if (blindedCD <= 0)
            {
                ResetBlind();
            }
            blindedCD -= Time.deltaTime;
        }
    }

    public void getDebuff (float debuffCooldown,float speedConstant)
    {
        if (!isDebuffed)
        {
            //Debug.Log("Got debuffed");
            debuffProportion /= speedConstant;
            isDebuffed = true;
            debuffCD = debuffCooldown;
            debuffFX.Play();
        }

    }

    public void resetDebuff()
    {
        isDebuffed = false;
        //Debug.Log("Reset Debuff");
        debuffProportion = 1f;
        if (debuffFX != null)
        {
            debuffFX.Stop();
            debuffFX.Clear();
        }
    }

    public void GetBlind(float blindedCooldown, float blindConstant)
    {
        if (!isBlinded)
        {
            isBlinded = true;
            //Debug.Log("Got blinded");
            blindProportion = blindConstant;
            blindedCD = blindedCooldown;
        }
    }

    public void ResetBlind()
    {
        //Debug.Log("BlindReset");
        isBlinded = false;
        blindProportion = 1;
    }

    void Shoot() {
        if (target == null) return;
        int RNG = Random.Range(0, 100);
        //Debug.Log("RNG is " + RNG);
        //Debug.Log("Threshold is " + blindProportion * 100);
        if (RNG <= (int)((float)blindProportion * 100))
            turret.Shoot(this, target);
    }

    public void ShowRange()
    {
        line.enabled = true;
    }

    public void HideRange()
    {
        line.enabled = false;
    }
}
