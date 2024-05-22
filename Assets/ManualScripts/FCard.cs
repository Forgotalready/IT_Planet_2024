using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCard : MonoBehaviour, ICard
{
    private bool isFClicked = false;
    public bool userLogic()
    {
        if (Input.GetKeyDown(KeyCode.F)) isFClicked = true;
        return isFClicked;
    }
}
