using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftCard : MonoBehaviour, ICard
{
    private bool isShiftClicked = false;
    public bool userLogic()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) isShiftClicked = true;
        return isShiftClicked;
    }
}
