using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangingInteraction : MonoBehaviour, IInteractable
{
    private Outline _outline;
    private CinemachineVirtualCamera _virtualCamera;
    private bool _isInteract = false;

    private Animator _animator;
    private bool _animatorOnObject;

    private FearLevel _fearLevel;
    private bool _fearLeveOnObject;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        _virtualCamera.enabled = false;

        _animatorOnObject = TryGetComponent<Animator>(out _animator);
        _fearLeveOnObject = TryGetComponent<FearLevel>(out _fearLevel);
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

        if(_animatorOnObject)
        {
            _animator.SetBool("Enter", _isInteract);
        }

        if(_fearLeveOnObject)
        {
            _fearLevel.enabled = _isInteract;
        }
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
