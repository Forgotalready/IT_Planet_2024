using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class MouseInputController : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 2f;
    private GameObject _interactableObject;
    private GameObject _previousInteractableObject;
    [SerializeField] private Camera _mainCamera;

    private void OnEnable()
    {
        InputManager.inputActions.InteractionObject.Interact.started += Interact;
    }


    private void OnDisable()
    {
        InputManager.inputActions.InteractionObject.Interact.started -= Interact;
    }
    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_interactableObject != null)
        {
            _interactableObject.gameObject.GetComponent<IInteractable>().Interact();
        }
    }
    private void Update()
    {
        if (InputManager.inputActions.InteractionObject.enabled)
        {
            InteractionRay();
        }
    }

    private void InteractionRay()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactionDistance) && hit.collider.tag == "Interactable")
        {
            _interactableObject = hit.collider.gameObject;

            if (_interactableObject != null && _interactableObject != _previousInteractableObject)
            {
                _interactableObject.gameObject.GetComponent<IInteractable>().OutlineEnable();
                _previousInteractableObject = _interactableObject;
            }
        }
        else
        {
            if (_previousInteractableObject != null)
            {
                _previousInteractableObject.gameObject.GetComponent<IInteractable>().OutlineDisable();
                _previousInteractableObject = null;
            }
            _interactableObject = null;
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }
}
