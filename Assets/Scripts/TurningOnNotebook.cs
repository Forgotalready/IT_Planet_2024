using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningOnNotebook : MonoBehaviour
{
    private void OnEnable()
    {
        ChoseObjectInteract.doorUnlock += ShowHallway;
    }
    private void OnDisable()
    {
        ChoseObjectInteract.doorUnlock -= ShowHallway;
    }

    void ShowHallway(string id)
    {
        if (id == "notepadOff")
        {
            transform.gameObject.SetActive(false);
            
        }
    }
}
