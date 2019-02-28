using UnityEngine;

public class Shop : MonoBehaviour {

    public LevelShop levelShop;

    void Start()
    {
        levelShop.CreateShop(transform);
    }

    public void BuyTurret(string tag)
    {
        levelShop.BuyTurret(tag);
    }
}
