using UnityEngine;

public class Shop : MonoBehaviour {

    public void BuyTurret(string tag) {
        BuildManager.bm.SetTurretToBuild(tag);
    }
}
