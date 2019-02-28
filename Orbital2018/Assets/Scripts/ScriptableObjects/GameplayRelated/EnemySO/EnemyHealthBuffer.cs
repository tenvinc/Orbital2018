using UnityEngine;
using System.Collections.Generic;
using TMPro;

[CreateAssetMenu(fileName = "EnemyHealthBuffer", menuName = "EnemySkills/EnemyHealthBuffer")]
public class EnemyHealthBuffer : EnemySkills
{

    public float speedConstant = 2f;
    // private string ownTag;

    public override void TriggerEnemySkill(Transform enemy)
    {
        // ownTag = enemy.tag;
        Collider[] withinRange = Physics.OverlapSphere(enemy.position, aoeRange * scaleProportion);
        List<Collider> enemiesWithinRange = new List<Collider>();
        foreach (Collider obj in withinRange)
        {
            // Debug.Log("Collided with " + obj.tag);
            if (tagmasterso.Tags.Contains(obj.tag))
            {
                enemiesWithinRange.Add(obj);
            }
        }
        // Debug.Log("Not Executing");
        if (enemiesWithinRange.Count > 0)
        {
            enemy.GetComponent<Enemy>().getBuff(999);
            for (int i = 0; i < enemiesWithinRange.Count; i++)
            {
                Collider enemyWithinRange = enemiesWithinRange[i];
                enemyWithinRange.GetComponent<Enemy>().getBuff(999);
            }
            // Debug.Log("Executing");
            // Debug.Log(Time.time);
            // Debug.Log(nextFireTime);
            // int RNG = Random.Range(0, enemiesWithinRange.Count);
            // Collider unluckyTower = enemiesWithinRange[RNG];
            // float debuffCooldown;

            // Debug.Log("Casting");

            // debuffCooldown = cooldownTime / 2;

            // unluckyTower.GetComponent<TurretShooting>().getDebuff(debuffCooldown, speedConstant);


        }

    }


}
