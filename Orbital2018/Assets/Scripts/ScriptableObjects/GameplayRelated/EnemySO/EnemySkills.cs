using UnityEngine;

[CreateAssetMenu(fileName = "NoSkill", menuName = "EnemySkills/NoSkill")]
public class EnemySkills : ScriptableObject {

    [Header("Enemy AOE")]
    public int aoeRange; // WIP requires a physical indicator of how long the range is
    public float scaleProportion = 0.2f;

    [Header("Enemy Skills")]
    public float cooldownTime;
    public float additionalTime = 0f;

    public TagMasterSO tagmasterso;

    // By default there is no skill
    public virtual void TriggerEnemySkill(Transform enemy)
    {
    }
}
