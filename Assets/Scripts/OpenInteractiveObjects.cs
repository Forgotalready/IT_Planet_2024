using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OpenInteractiveObjects : MonoBehaviour, IInteractable
{
    private Outline _outline;
    private bool _isOpen = false;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0f;
    }
    public void OutlineEnable()
    {
        _outline.OutlineWidth = 2f;
    }

    public void OutlineDisable() 
    {
        _outline.OutlineWidth = 0f;
    }
    public void Interact()
    {
        _isOpen = !_isOpen;
        GetComponent<Animator>().SetBool("Open", _isOpen);
        GetComponent<Animator>().SetBool("Close", !_isOpen);
    }

    public string GetDescription()
    {
        return "";
    }
}
