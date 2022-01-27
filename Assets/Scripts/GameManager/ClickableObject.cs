using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public int index;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.instance.DisplayEquipementDescriptionPanel(index, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.instance.DisplayEquipementDescriptionPanel(index, false);
    }
}
