using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
    public void OutlineEnable();
    public void OutlineDisable();
    string GetDescription();

}
