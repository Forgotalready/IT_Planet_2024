using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemPickUp : MonoBehaviour, IInteractable
{
    public Item Item;
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0f;
    }
    void PickUp(){
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    public void Interact()
    {
        PickUp();

        InventoryManager.Instance.clearInvetory();

        InventoryManager.Instance.ListItems();
    }

    public void OutlineEnable()
    {
        _outline.OutlineWidth = 2f;
    }

    public void OutlineDisable()
    {
        _outline.OutlineWidth = 0f;
    }

    public string GetDescription()
    {
        return "";
    }
}
