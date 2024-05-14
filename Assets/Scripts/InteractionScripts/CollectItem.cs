using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        _outline.enabled = false;
    }
    void PickUp()
    {
        _outline.enabled = false;
        Destroy(gameObject);
    }

    public void Interact()
    {
        PickUp();
        var result = _inventoryEntryPoint.AddItem(_name, _amount, _image);
    }

    public void OutlineEnable()
    {
        _outline.enabled = true;
    }

    public void OutlineDisable()
    {
        _outline.enabled = false;
    }

    public string GetDescription()
    {
        return "";
    }
}
