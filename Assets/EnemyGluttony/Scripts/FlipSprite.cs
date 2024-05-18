using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlipSprite : MonoBehaviour
{
    [SerializeField] private Transform _targetTransformVisual;
    [SerializeField] private float _lerpCoeff;
    private Animator _animator;


    private Transform enemyObject;
    private List<Vector3> enemyDirections;

    private float _rotationAlongY = 0f;
    private float _rotationAngle = 90f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        enemyObject = transform.parent;
    }

    private void OnEnable()
    {
        InputManager.inputActions.Gameplay.RotationRight.started += DoRotationRigth;
        InputManager.inputActions.Gameplay.RotationLeft.started += DoRotationLeft;
    }

    private void OnDisable()
    {
        InputManager.inputActions.Gameplay.RotationRight.started -= DoRotationRigth;
        InputManager.inputActions.Gameplay.RotationLeft.started -= DoRotationLeft;
    }

    void Update()
    {
        transform.rotation = _targetTransformVisual.rotation;
        Vector3 viewVector;

        switch (_rotationAlongY)
        {
            case 0f:
                viewVector = Vector3.forward;
                break;
            case 90f:
                viewVector = Vector3.right;
                break;
            case 180:
                viewVector = Vector3.back;
                break;
            default:
                viewVector = Vector3.left;
                break;
        }

        enemyDirections = new List<Vector3>
        {
            enemyObject.forward,
            enemyObject.right,
            enemyObject.forward * -1,
            enemyObject.right * -1,
        };

        float maxPositiveDot = 0f;
        int sideIndex = 0;

        for (int i = 0; i < enemyDirections.Count; i++)
        {
            float dot = Vector3.Dot(enemyDirections[i], viewVector);
            if (dot > maxPositiveDot)
            {
                maxPositiveDot = dot;
                sideIndex = i;
            }
        }

        switch (sideIndex)
        {
            case 0:
                _animator.SetBool("Side", false);
                _animator.SetBool("Behind", true);
                break;
            case 1:
                _animator.SetBool("Side", true);
                _animator.SetBool("Behind", false);
                GetComponent<SpriteRenderer>().flipX = false;
                break;
            case 2:
                _animator.SetBool("Side", false);
                _animator.SetBool("Behind", false);
                break;
            case 3:
                _animator.SetBool("Side", true);
                _animator.SetBool("Behind", false);
                GetComponent<SpriteRenderer>().flipX = true;
                break;
            default:
                break;

        }

        Debug.DrawRay(enemyObject.position, enemyDirections[0], Color.red);
        Debug.DrawRay(enemyObject.position, enemyDirections[1], Color.green);
        Debug.DrawRay(enemyObject.position, enemyDirections[2], Color.yellow);
        Debug.DrawRay(enemyObject.position, enemyDirections[3], Color.blue);

        enemyDirections.Clear();
    }

    private void DoRotationLeft(InputAction.CallbackContext context)
    {
        _rotationAlongY -= _rotationAngle;
        if (_rotationAlongY < 0)
            _rotationAlongY = 360 - _rotationAngle;
        if (_rotationAlongY == 360)
            _rotationAlongY = 0;
    }

    private void DoRotationRigth(InputAction.CallbackContext context)
    {
        _rotationAlongY += _rotationAngle;
        if (_rotationAlongY > 360)
            _rotationAlongY = _rotationAngle;
        if (_rotationAlongY == 360)
            _rotationAlongY = 0;
    }

}
