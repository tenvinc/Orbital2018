using UnityEngine;
using UnityEngine.EventSystems;

public class DescribeCodeInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CodeInfo myCode;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show tooltip
        InterfaceManager.ui.ShowTooltip(transform.position, myCode);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide tooltip
        InterfaceManager.ui.HideTooltip();
    }
}
