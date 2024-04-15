using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInteractiveObjects : MonoBehaviour, IInteractable
{
    private Outline _outline;
    private bool isOpen = false;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }
    private void OnTriggerEnter(Collider collision)
    {
        _outline.enabled = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        _outline.enabled = false;
    }
    public void Interact()
    {
        isOpen = !isOpen;
        GetComponent<Animator>().SetBool("Open", isOpen);
        GetComponent<Animator>().SetBool("Close", !isOpen);
    }

    public string GetDescription()
    {
        if (isOpen) return "Выкл";
        return "Вкл";
    }
}
