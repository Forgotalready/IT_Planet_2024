using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class applicationObject : MonoBehaviour, IInteractable
{
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0f;
    }

    public string GetDescription()
    {
        return "";
    }

    public void Interact()
    {
        return;
    }

    public void OutlineDisable()
    {
        _outline.OutlineWidth = 0f;
    }

    public void OutlineEnable()
    {
        _outline.OutlineWidth = 2f;
    }
}
