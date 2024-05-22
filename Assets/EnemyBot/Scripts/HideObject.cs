using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideObject : MonoBehaviour, IInteractable
{
    public event Action playerHide;

    private Outline _outline;

    public string GetDescription()
    {
        return "";
    }

    public void Interact()
    {
        playerHide?.Invoke();
    }

    public void OutlineDisable()
    {
        _outline.enabled = false;
    }

    public void OutlineEnable()
    {
        _outline.enabled = true;
    }

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }
}
