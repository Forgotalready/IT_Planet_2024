using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKitchen : MonoBehaviour
{
    private void OnEnable()
    {
        ChoseObjectInteract.doorUnlock += ShowKitchenAnim;
    }
    private void OnDisable()
    {
        ChoseObjectInteract.doorUnlock -= ShowKitchenAnim;
    }

    void ShowKitchenAnim(string id)
    {
        if (id == "DOOR")
        {
            GetComponent<Animator>().SetBool("Show", true);
        }
    }
}
