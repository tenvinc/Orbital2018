using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TowerSpeedReducer", menuName = "EnemySkills/TowerSpeedReducer")]
public class TowerSpeedReducer : EnemySkills {

    public float speedConstant = 2f;
    public float debuffDurationProportion = 0.75f;

    public override void TriggerEnemySkill(Transform enemy)
    {
        Collider[] withinRange = Physics.OverlapSphere(enemy.position, aoeRange * scaleProportion);
        List<Collider> towersWithinRange = new List<Collider>();
        foreach (Collider obj in withinRange)
        {
            // Debug.Log("Collided with " + obj.tag);
            if (tagmasterso.towerTags.Contains(obj.tag))
            {
                // Debug.Log("Collided with " + obj.tag);
                towersWithinRange.Add(obj);
            }
        }
        if (towersWithinRange.Count > 0)
        {
            // Debug.Log("Executing");
            // Debug.Log(Time.time);
            // Debug.Log(nextFireTime);
            int RNG = Random.Range(0, towersWithinRange.Count);
            int RNGindex = RNG;
            Collider unluckyTower = towersWithinRange[RNGindex];
            while (unluckyTower.GetComponent<TurretShooting>().isDebuffed)
            {
                RNGindex = (RNGindex + 1) % towersWithinRange.Count;
                if (RNGindex != RNG)
                    unluckyTower = towersWithinRange[RNGindex];
                else break;
            }
            float debuffCooldown;
            
            debuffCooldown = cooldownTime * debuffDurationProportion;
            unluckyTower.GetComponent<TurretShooting>().getDebuff(debuffCooldown,speedConstant);
            

        }

    }


}
