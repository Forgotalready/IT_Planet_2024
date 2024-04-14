using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveWalls : MonoBehaviour
{
    private GameObject hittedObject;
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject newObject = hit.collider.gameObject;
            Debug.Log(newObject.tag);
            if (newObject.tag == "Wall")
            {
                if (hittedObject == null)
                {
                    hittedObject = newObject;
                    hittedObject.SetActive(false);
                    deactivateChilds(hittedObject);
                }
                else if (hittedObject != newObject) {
                    hittedObject.SetActive(true);
                    activateChilds(hittedObject);

                    newObject.SetActive(false);
                    deactivateChilds(newObject);
                }
            }
        }
    }

    void deactivateChilds(GameObject wall) {
        Transform wallTransform = wall.transform;
        foreach(Transform child in wallTransform) { 
            child.gameObject.SetActive(false);
        }
    }

    void activateChilds(GameObject wall) {
        Transform wallTransform = wall.transform;
        foreach (Transform child in wallTransform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
