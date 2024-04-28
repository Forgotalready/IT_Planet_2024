using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showHallway : MonoBehaviour
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
        if (id == "DOOR")
        {
            GetComponent<Animator>().SetBool("Show", true);
        }
    }
}
