using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    [SerializeField] private Transform _targetTransformVisual;
    void LateUpdate()
    {
        transform.rotation = _targetTransformVisual.rotation;
    }
}
