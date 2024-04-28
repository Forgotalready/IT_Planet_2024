using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject interactObject = GameState.Instance.choseObject;

        if (interactObject != null) { 
            ChoseObjectInteract interact = interactObject.GetComponent<ChoseObjectInteract>();
            if (interact != null)
            {
                interact.Interact(eventData.pointerClick.GetComponent<Item>());
            }
        }
    }
}
