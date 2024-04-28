using System;
using System.Collections.Generic;
using UnityEngine;

public class ChoseObjectInteract : MonoBehaviour
{

    [SerializeField] private List<ItemEnum> _canInteract;
    public static Action<string> doorUnlock;

    public void Interact(Item interactor)
    {

        if (_canInteract.Contains((ItemEnum)interactor.id))
        {
            doorUnlock?.Invoke(transform.gameObject.name);
            InventoryManager.Instance.Remove(interactor.id);
        }
        
    }
}
