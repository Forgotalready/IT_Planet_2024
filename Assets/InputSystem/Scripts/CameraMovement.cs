using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _targetTransformVisual;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _lerpRate = 3f;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _targetTransform.position, Time.deltaTime * _lerpRate);
        transform.rotation = _targetTransformVisual.rotation;
    }
}
