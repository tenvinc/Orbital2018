using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LevelShop", menuName = "LevelShops/ShopForLevel")]
public class LevelShop : ScriptableObject {

    public TurretShop[] shopList;

    public void CreateShop(Transform shop)
    {
        for (int i=0; i<shopList.Length; i++)
        {
            GameObject instance = Instantiate(shopList[i].iconPrefab);
            instance.transform.SetParent(shop,false);
            instance.GetComponent<TurretShopButton>().SetShop(shop);
        }
    }

    public void BuyTurret(string tag)
    {
        for (int i = 0; i < shopList.Length; i++)
        {
            if (shopList[i].iconPrefab.tag == tag)
            {
                BuildManager.bm.SetTurretToBuild(shopList[i].blueprint);
                return;
            }
        }
    }
}
