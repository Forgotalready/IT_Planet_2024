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
    [SerializeField] private string _sprint = "Sprint";
    [SerializeField] private string _interact = "Interact";
    [SerializeField] private string _rotateRight = "RotationRight";
    [SerializeField] private string _rotateLeft = "RotationLeft";
    [SerializeField] private string _sit = "Sit";

    private InputAction _moveAction;
    private InputAction _sprintAction;
    private InputAction _interactAction;
    private InputAction _rotateActionRight;
    private InputAction _rotateActionLeft;
    private InputAction _switchActionMap;
    private InputAction _sitAction;

    public static Action<string> OnSwitch;

    public Vector2 moveInput { get; private set; }
    public float sprintValue { get; private set; }
    public bool interactTriggered { get; set; }
    public bool rotateRightTriggered { get; set; }
    public bool rotateLeftTriggered { get; set; }
    public string actionMapName { get; private set; }
    public bool sitTrigger {  get; private set; }

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
        _sprintAction = _playerControls.FindActionMap(_actionMapName).FindAction(_sprint);
        _interactAction = _playerControls.FindActionMap(_actionMapName).FindAction(_interact);
        _rotateActionRight = _playerControls.FindActionMap(_actionMapName).FindAction(_rotateRight);
        _rotateActionLeft = _playerControls.FindActionMap(_actionMapName).FindAction(_rotateLeft);
        actionMapName = _actionMapName;
        _switchActionMap = _playerControls.FindActionMap(_secondActionMapName).FindAction(_interact);
        _sitAction = _playerControls.FindActionMap(_actionMapName).FindAction(_sit);
        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        _moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        _moveAction.canceled += context => moveInput = Vector2.zero;

        _sprintAction.performed += context => sprintValue = context.ReadValue<float>();
        _sprintAction.canceled += context => sprintValue = 0f;

        _interactAction.performed += context => interactTriggered = true;
        _switchActionMap.performed += context => interactTriggered = true;
        _switchActionMap.performed += SwitchActionMap;
        _interactAction.performed += SwitchActionMap;
        

        _rotateActionRight.performed += context => rotateRightTriggered = true;
        _rotateActionLeft.performed += context => rotateLeftTriggered = true;

        _sitAction.performed += context => sitTrigger = true;
        _sitAction.canceled += context => sitTrigger = false;
    }

    private void SwitchActionMap(InputAction.CallbackContext context)
    {
        OnSwitch?.Invoke("InteractionObject");
    }



    private void OnEnable()
    {
        _moveAction.Enable();
        _sprintAction.Enable();
        _interactAction.Enable();
        _rotateActionRight.Enable();
        _rotateActionLeft.Enable();
        _sitAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _sprintAction.Disable();
        _interactAction.Disable();
        _rotateActionRight.Disable();
        _rotateActionLeft.Disable();
        _sitAction.Disable();
    }

}
