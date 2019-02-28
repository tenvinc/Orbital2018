using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu (menuName = "TagMaster")]
public class TagMasterSO : ScriptableObject {

	[Header("Tags")]
    public string EnemyTag;
    public string BossTag;
    public string DummyTag;
    public string CodeShopTag;
    public string FireTowerTag;
    public string MortarTag;
    public string JammingDroneTag;
    public string FasterEnemyTag;

    [Header("Classification for VP")]
    public string ActionTag;
    public string ConditionTag;
    public string elseTag;
    public string ifTag;

    public List<string> Tags;
    public List<string> towerTags;

}
