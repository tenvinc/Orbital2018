using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerBlinding", menuName = "EnemySkills/TowerBlinding")]
public class TowerBlinding : EnemySkills{

    public float blindConstant = 2f;

    public override void TriggerEnemySkill(Transform enemy)
    {
        Collider[] withinRange = Physics.OverlapSphere(enemy.position, aoeRange*scaleProportion);
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
        // Debug.Log("Not Executing");
        Debug.Log(towersWithinRange.Count);
        if (towersWithinRange.Count > 0)
        {
            int RNG = Random.Range(0, towersWithinRange.Count);
            Collider unluckyTower = towersWithinRange[RNG];
            float blindCooldown;

            blindCooldown = cooldownTime / 2;
            unluckyTower.GetComponent<TurretShooting>().GetBlind(blindCooldown, blindConstant);
        }


    }


}
