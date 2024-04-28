using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveWalls : MonoBehaviour
{
    private GameObject hittedObject;
    [SerializeField] private float _rayDistance = 5f;
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _rayDistance))
        {
            GameObject newObject = hit.collider.gameObject;
            if (newObject.tag == "Wall")
            {
                if (hittedObject == null)
                {
                    hittedObject = newObject;

                    MeshRenderer meshRenderer = hittedObject.GetComponent<MeshRenderer>();
                    if (meshRenderer != null)
                    {
                        meshRenderer.enabled = false;
                    }

                    deactivateChilds(hittedObject);
                }
                else if (hittedObject != newObject)
                {
                    MeshRenderer meshRenderer = hittedObject.GetComponent<MeshRenderer>();
                    if (meshRenderer != null)
                    {
                        meshRenderer.enabled = true;
                    }
                    activateChilds(hittedObject);

                    meshRenderer = newObject.GetComponent<MeshRenderer>();
                    if (meshRenderer != null)
                    {
                        meshRenderer.enabled = false;
                    }
                    deactivateChilds(newObject);

                    hittedObject = newObject;
                }
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
