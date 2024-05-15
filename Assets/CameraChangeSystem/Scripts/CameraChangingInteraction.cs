using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangingInteraction : MonoBehaviour, IInteractable
{
    private Outline _outline;
    private CinemachineVirtualCamera _virtualCamera;
    private bool _isInteract = false;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        _virtualCamera.enabled = false;
    }

    public string GetDescription()
    {
        return "";
    }

    public void Interact()
    {
        _isInteract = !_isInteract;
        _virtualCamera.enabled = _isInteract;
        if(_isInteract)
            _outline.enabled = false;
        else
            _outline.enabled = true;
    }

    public void OutlineDisable()
    {
        _outline.enabled = false;
    }

    public void OutlineEnable()
    {
        _outline.enabled = true;
    }
}
