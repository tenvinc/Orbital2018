using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMakeInsane", menuName = "EnemySkills/EnemyMakeInsane")]
public class EnemyMakeInsane : EnemySkills
{
    public float speedBuffProportion = 1.2f;
    public int RNGconstant = 50;

    public ShakeTransformEventData data;

    public GameObject[] Aberrants;

    public override void TriggerEnemySkill(Transform enemy)
    {
        Camera.main.GetComponent<ShakeTransform>().AddShakeEvent(data);
        List<GameObject> enemies = new List<GameObject>();
        for (int i = 0; i < tagmasterso.Tags.Count; i++)
        {
            string currTag = tagmasterso.Tags[i];
            if (currTag == enemy.tag) continue;
            GameObject[] temp = GameObject.FindGameObjectsWithTag(currTag);
            for (int j = 0; j < temp.Length; j++)
            {
                enemies.Add(temp[j]);
            }
        }
        for (int i=0; i < enemies.Count; i++)
        {
            GameObject currEnemy = enemies[i];
            if (currEnemy.tag == tagmasterso.Tags[0])
            {
                int RNG = Random.Range(0, 100);
                if (RNG > RNGconstant)
                {
                    int mutationRNG = Random.Range(0, Aberrants.Length);
                    GameObject mutatedEnemy = Instantiate(Aberrants[mutationRNG], currEnemy.transform.position, currEnemy.transform.rotation);
                    mutatedEnemy.GetComponent<EnemyMove>().setNextNode(currEnemy.GetComponent<EnemyMove>().nextNodeIndex);
                    Destroy(currEnemy);
                    currEnemy = mutatedEnemy;
                }                
            }
            currEnemy.GetComponent<Enemy>().MakeInsane(speedBuffProportion);          
        }
    }
}
