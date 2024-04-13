using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset _playerControls;

    [Header("Action Map Name")]
    [SerializeField] private string _actionMapName = "Player";
    [SerializeField] private string _secondActionMapName = "InteractionObject";

    [Header("Action Name")]
    [SerializeField] private string _move = "Move";
    [SerializeField] private string _jump = "Jump";
    [SerializeField] private string _sprint = "Sprint";
    [SerializeField] private string _interact = "Interact";
    [SerializeField] private string _rotate = "Rotation";

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;
    private InputAction _interactAction;
    private InputAction _rotateAction;
    private InputAction _switchActionMap;

    public static Action<string> OnSwitch;

    public Vector2 moveInput { get; private set; }
    public bool jumpTriggered { get; private set; }
    public float sprintValue { get; private set; }
    public bool interactTriggered { get; set; }
    public bool rotateTriggered { get; set; }
    public string actionMapName { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_move);
        _jumpAction = _playerControls.FindActionMap(_actionMapName).FindAction(_jump);
        _sprintAction = _playerControls.FindActionMap(_actionMapName).FindAction(_sprint);
        _interactAction = _playerControls.FindActionMap(_actionMapName).FindAction(_interact);
        _rotateAction = _playerControls.FindActionMap(_actionMapName).FindAction(_rotate);
        actionMapName = _actionMapName;
        _switchActionMap = _playerControls.FindActionMap(_secondActionMapName).FindAction(_interact);

        Debug.Log(_switchActionMap);
        

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        _moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        _moveAction.canceled += context => moveInput = Vector2.zero;

        _jumpAction.performed += context => jumpTriggered = true;
        _jumpAction.canceled += context => jumpTriggered = false;

        _sprintAction.performed += context => sprintValue = context.ReadValue<float>();
        _sprintAction.canceled += context => sprintValue = 0f;

        _interactAction.performed += context => interactTriggered = true;
        _switchActionMap.performed += SwitchActionMap;
        _interactAction.performed += SwitchActionMap;
        

        _rotateAction.performed += context => rotateTriggered = true;
    }

    private void SwitchActionMap(InputAction.CallbackContext context)
    {
        OnSwitch?.Invoke("InteractionObject");
        Debug.Log("Я вызываюсь");
    }



    private void OnEnable()
    {
        _moveAction.Enable();
        _jumpAction.Enable();
        _sprintAction.Enable();
        _interactAction.Enable();
        _rotateAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _jumpAction.Disable();
        _sprintAction.Disable();
        _interactAction.Disable();
        _rotateAction.Disable();
    }

}
