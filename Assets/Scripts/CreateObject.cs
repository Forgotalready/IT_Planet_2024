using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{

    private void OnEnable()
    {
        ChoseObjectInteract.doorUnlock += Create;
    }
    private void OnDisable()
    {
        ChoseObjectInteract.doorUnlock -= Create;
    }

    void Create(string id)
    {
        if (id == "table")
        {
            InventoryManager.Instance.Add(GameState.Instance.Item[3]);
            GetComponent<Outline>().enabled = false;
        }
            

    }
}
