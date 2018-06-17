using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu (menuName = "TagMaster")]
public class TagMasterSO : ScriptableObject {

	[Header("Tags")]
    public string EnemyTag;
    public string BossTag;
    public string DummyTag;

    public List<string> Tags = new List<string>();

    public void Initialize ()
    {
        Tags.Add(EnemyTag);
        Tags.Add(BossTag);
    }

}
