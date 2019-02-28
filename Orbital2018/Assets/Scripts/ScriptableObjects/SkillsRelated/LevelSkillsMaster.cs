using UnityEngine;

[CreateAssetMenu(fileName = "LevelSkillsMaster", menuName = "Skills/LevelSkillsMaster")]
public class LevelSkillsMaster: ScriptableObject {

    public GameObject CSprefab;
    public TowerSkills[] towers;

    public void CreateLevelCodeShops(Transform CodeShopBG)
    {
        for (int i=0; i<towers.Length; i++)
        {
            GameObject CS = (GameObject)Instantiate(CSprefab);
            CS.transform.SetParent(CodeShopBG.transform, false);
            GameObject[] skillsList = towers[i].skillsList;
            for (int j=0; j<skillsList.Length; j++)
            {
                GameObject prefab = skillsList[j];
                GameObject instance = (GameObject)Instantiate(prefab);
                instance.transform.SetParent(CS.transform, false);
            }
            InterfaceManager.ui.EnterNewValue(towers[i].tag, CS.transform);
            Transform hiddencanvas = InterfaceManager.ui.hiddenUI;
            CS.transform.SetParent(hiddencanvas, false);
        }
    }
}
