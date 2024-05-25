using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveWalls : MonoBehaviour
{
    private GameObject hittedObject;
    private float _rayDistance = 5f;
    [SerializeField] private GameObject Player;

    private void OnEnable()
    {
        InputManager.inputActions.Gameplay.RotationLeft.started += leftCameraRotate;
    }

    private void leftCameraRotate(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    void FixedUpdate()
    {
        _rayDistance = Vector3.Distance(transform.position, Player.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _rayDistance))
        {
            GameObject newObject = hit.collider.gameObject;
            if (newObject.tag == "Wall")
            {
                if (hittedObject == null)
                {
                    hittedObject = newObject;
                    deactivateChilds(hittedObject);
                    
                    //MeshRenderer meshRenderer = hittedObject.GetComponent<MeshRenderer>();
                    //if (meshRenderer != null)
                    //{
                    //    meshRenderer.enabled = false;
                    //}

                }
                else if (hittedObject != newObject)
                {
                    //MeshRenderer meshRenderer = hittedObject.GetComponent<MeshRenderer>();
                    //if (meshRenderer != null)
                    //{
                    //    meshRenderer.enabled = true;
                    //}
                    activateChilds(hittedObject);
                    //meshRenderer = newObject.GetComponent<MeshRenderer>();
                    //if (meshRenderer != null)
                    //{
                    //    meshRenderer.enabled = false;
                    //}
                    deactivateChilds(newObject);
                    hittedObject = newObject;
                }
            }
            else
            {
                if(hittedObject != null)
                {
                    activateChilds(hittedObject);
                }
                hittedObject = null;
            }
        }
    }

    void deactivateChilds(GameObject wall)
    {
        Transform wallTransform = wall.transform;
        foreach (Transform child in wallTransform)
        {
            GameObject childObject = child.gameObject;
            deactivateChilds(childObject);
            MeshRenderer meshRenderer = childObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
        }
    }

    void activateChilds(GameObject wall)
    {
        Transform wallTransform = wall.transform;
        foreach (Transform child in wallTransform)
        {
            GameObject childObject = child.gameObject;
            activateChilds(childObject);
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
        }
    }
}
