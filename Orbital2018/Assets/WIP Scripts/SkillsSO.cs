using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "SkillsSO")]
public class SkillsSO : ScriptableObject {

    TowerSO towerSO;
    EnemiesSO enemiesSO;

    [Header("Skills Duration")]
    public int duration = 3;

    public void InitializeSkills ()
    {
        towerSO = (TowerSO)ScriptableObject.CreateInstance(typeof(TowerSO));
        enemiesSO = (EnemiesSO)ScriptableObject.CreateInstance(typeof(EnemiesSO));
    }

    public void Berserk () 
    {
        towerSO.fireRate *= 2;
        SkillsSOMaster.instance.StartCoroutine(TimeElapsed());
    }

    public void Freeze ()
    {
        enemiesSO.speed = 0f;
        SkillsSOMaster.instance.StartCoroutine(TimeElapsed());
    }

    IEnumerator TimeElapsed ()
    {
        yield return new WaitForSeconds(duration);
    }

}
