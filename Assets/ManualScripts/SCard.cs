using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCard : MonoBehaviour, ICard
{
    private bool isSClicked = false;
    public bool userLogic()
    {
        if (Input.GetKeyDown(KeyCode.S)) isSClicked = true;
        return isSClicked;
    }
}
