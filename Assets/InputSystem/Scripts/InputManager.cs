using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    public static PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
        ToggleActionMap(inputActions.Gameplay);
    }

    private void OnEnable()
    {
        //inputActions = new PlayerControls();
        //ToggleActionMap(inputActions.Gameplay);
    }
    private void Start()
    {
        //inputActions = new PlayerControls();
        //ToggleActionMap(inputActions.Gameplay);
    }

    public static void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
            return;

        inputActions.Disable();
        actionMap.Enable();
    }
}
