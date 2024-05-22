using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Manual : MonoBehaviour
{
    [SerializeField] GameObject movementPanel;
    [SerializeField] GameObject sittingPanel;
    [SerializeField] GameObject rotationCameraPanel;
    [SerializeField] GameObject hidePanel;


    void Start(){
        movementPanel.SetActive(true);
       

    }

  

    void OnEnable(){
        InputManager.inputActions.Gameplay.Movement.started += HideManualMovement;
    }
    

    void OnDisable(){
        InputManager.inputActions.Gameplay.Movement.started -= HideManualMovement;
    }
   

    private void HideManualMovement(InputAction.CallbackContext context){
        movementPanel.SetActive(false);
    }

    private void ShowManual(InputAction.CallbackContext context){
        movementPanel.SetActive(false);
    }

}
