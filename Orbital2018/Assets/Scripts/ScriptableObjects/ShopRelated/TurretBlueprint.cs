using UnityEngine;

[CreateAssetMenu(fileName = "Blueprint", menuName = "TurretBP/Blueprint")]
public class TurretBlueprint : ScriptableObject{

    public GameObject prefab;
    public int cost;

    public int GetSellAmount()
    {
        return cost;
    }

}
