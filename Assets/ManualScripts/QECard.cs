using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QECard : MonoBehaviour, ICard
{
    private bool isQClicked = false;
    private bool isEClicked = false;
    public bool userLogic()
    {
        if (Input.GetKeyDown(KeyCode.E)) isEClicked = true;
        if (Input.GetKeyDown(KeyCode.Q)) isQClicked = true;
        return (isQClicked && isEClicked);
    }
}
