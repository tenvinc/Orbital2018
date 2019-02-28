using UnityEngine;

public class CodeShop : MonoBehaviour {

    public LevelSkillsMaster levelSkillsmaster;

	void Start () {
        levelSkillsmaster.CreateLevelCodeShops(transform);
	}
}
