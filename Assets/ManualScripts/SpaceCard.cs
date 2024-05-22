using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCard : MonoBehaviour, ICard
{
    private bool isSpaceClicked = false;
    public bool userLogic()
    {
        if (Input.GetKeyDown(KeyCode.Space)) isSpaceClicked = true;
        return isSpaceClicked;
    }
}
