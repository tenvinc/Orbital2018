using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint fireTower;

    public void BuyTurret() {
        BuildManager.bm.SetTurretToBuild(fireTower);
        //Debug.Log("buying");
    }
}
