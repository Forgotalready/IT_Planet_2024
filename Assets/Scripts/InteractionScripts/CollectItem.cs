using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectItem : MonoBehaviour, IInteractable
{
    private Outline _outline;
    [SerializeField] private EntryPoint _inventoryEntryPoint;
    [SerializeField] private string _name;
    [SerializeField] private int _amount;
    [SerializeField] private Sprite _image;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0f;
    }
    void PickUp()
    {
        Destroy(gameObject);
    }

    public void Interact()
    {
        PickUp();
        var result = _inventoryEntryPoint.AddItem(_name, _amount, _image);
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
