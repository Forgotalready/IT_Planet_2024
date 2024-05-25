using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        transform.position = new Vector3(transform.position.x , transform.position.y, transform.position.z - speed * Time.deltaTime);
    }
}
