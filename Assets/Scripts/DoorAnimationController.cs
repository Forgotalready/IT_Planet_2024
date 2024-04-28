using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    private void OnEnable()
    {
        ChoseObjectInteract.doorUnlock += OpenDoor;
    }
    private void OnDisable()
    {
        ChoseObjectInteract.doorUnlock -= OpenDoor;
    }

    void OpenDoor(string id)
    {
        if(id == "DOOR")
        {
            GetComponent<Animator>().SetBool("Open", true);
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
