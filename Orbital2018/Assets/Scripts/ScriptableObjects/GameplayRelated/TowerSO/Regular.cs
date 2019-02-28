using UnityEngine;

[CreateAssetMenu(fileName = "Regular", menuName = "Turret/Regular", order = 1)]
public class Regular : Turret
{
    public GameObject bulletPrefab;
    public WeightedAudioList sfxSelection;

    public override void Shoot(TurretShooting turret, Transform _target)
    {
        AudioClip choice = sfxSelection.PlayWeightedSelection();
        AudioSource turretAudio = turret.GetComponent<AudioSource>();
        turretAudio.clip = choice;
        if (choice != null) turretAudio.Play();
        turret.muzzleFX.Play();
        Transform firePoint = turret.firePoint;
        // GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bullet = ObjectPool.instance.GetPooledObject(0);
        if (bullet == null)
        {
            return;
        }
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().SetTarget(_target);
    }

    public override LineRenderer BuildRangeCircle(Transform turret)
    {
        LineRenderer line = turret.GetComponent<LineRenderer>();
        line.widthMultiplier = lineWidth;
        line.loop = true;
        line.positionCount = lineVtxCount;
        line.useWorldSpace = true;
        float deltaTheta = (2f * Mathf.PI) / lineVtxCount;
        float theta = 0f;
        for (int i=0; i<lineVtxCount; i++)
        {
            Vector3 pos = new Vector3(turret.position.x + turretRange * scaleProportion * Mathf.Cos(theta), 
                turret.position.y, turret.position.z + turretRange * scaleProportion * Mathf.Sin(theta));
            line.SetPosition(i, pos);
            theta += deltaTheta;
        }
        return line;
    }
}
