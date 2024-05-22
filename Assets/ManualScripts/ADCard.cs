using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADCard : MonoBehaviour, ICard
{

    private bool isAClicked = false;
    private bool isDClicked = false;
    public bool userLogic()
    {
        if (Input.GetKeyDown(KeyCode.A)) isAClicked = true;
        if (Input.GetKeyDown(KeyCode.D)) isDClicked = true;
        return (isAClicked && isDClicked);
    }
}
