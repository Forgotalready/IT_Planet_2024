using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    private Animator _animator;
    private bool _isHidden;

    public event Action playerHide;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        InputManager.inputActions.InteractionObject.Hide.performed += HidePerformed;
        InputManager.inputActions.InteractionObject.Hide.canceled += HideCanceled;
    }



    private void OnDisable()
    {
        InputManager.inputActions.InteractionObject.Hide.performed -= HidePerformed;
        InputManager.inputActions.InteractionObject.Hide.canceled -= HideCanceled;
    }

    private void HideCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isHidden = false;
        playerHide?.Invoke();
    }

    private void HidePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isHidden = true;
        playerHide?.Invoke();
    }

    private void Update()
    {
        _animator.SetBool("Hide", _isHidden);
    }
}
