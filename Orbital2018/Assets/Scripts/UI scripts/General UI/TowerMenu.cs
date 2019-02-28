using UnityEngine;

public class TowerMenu : MonoBehaviour {

	public void Sell()
    {
        InterfaceManager.ui.selectedNode.SellTurret();
        InterfaceManager.ui.towerMenu.SetActive(false);
        BuildManager.isDuplicated = false;
    }

    public void DisplayMenu()
    {
        Debug.Log("display");
    }

    public void Duplicate()
    {
        BuildManager.duplicatedBlueprint = InterfaceManager.ui.selectedNode.blueprint;
        BuildManager.duplicatedTowerconsole = InterfaceManager.ui.selectedNode.GetComponent<DisplayConsole>().towerConsole.gameObject;
        BuildManager.duplicatedCodeshop = InterfaceManager.ui.selectedNode.GetComponent<DisplayConsole>().codeShop.gameObject;
        BuildManager.duplicatedPrefab = InterfaceManager.ui.selectedNode.turret.gameObject;
        BuildManager.isDuplicated = true;
        InterfaceManager.ui.towerMenu.SetActive(false);
    }
}
