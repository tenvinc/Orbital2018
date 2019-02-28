using UnityEngine;
using UnityEngine.Events;

public class TurretShopButton : MonoBehaviour {

    private UnityAction buttonTapped;
    private Shop shop;

    void Start()
    {
        buttonTapped += BuyTurret;
    }

    void BuyTurret()
    {
        shop.BuyTurret(tag);
    }

    public void OnClick()
    {
        if (buttonTapped != null)
        {
            buttonTapped();
            BuildManager.isDuplicated = false;
        }
    }

    public void SetShop(Transform _shop)
    {
        shop = _shop.GetComponent<Shop>();
    }

}
