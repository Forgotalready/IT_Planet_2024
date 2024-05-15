using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualOff : MonoBehaviour
{
 
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("It's fucking work!");
            EventBus.onManuallOff?.Invoke();
        }
    }
   
}
