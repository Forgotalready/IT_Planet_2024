using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{

    private InputAction _moveAction;
    private InputAction _sprintAction;
    private InputAction _hidingAction;

    [SerializeField] GameObject _playerVisual;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [Header("Movement Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _sprintCoef = 1.5f;
    [SerializeField] private float _interactionDistance = 2f;

    private Vector3 forceDiraction = Vector3.zero;
    private Rigidbody _rigidbody;

    [SerializeField] private float _rotationSpeed = 10f;
    private float _rotationAngle = 90f;
    private float _rotationAlongY;

    [Header("Show/Hide Visual Settings")]
    [SerializeField] private float animationDuration = 1f;


    private GameObject _interactableObject;
    private GameObject _previousInteractableObject;

    private Vector3 _previousPosition;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = _playerVisual.GetComponent<Animator>();
        _spriteRenderer = _playerVisual.GetComponent<SpriteRenderer>();
        gameObject.GetComponent<MouseInputController>().enabled = false;
    }

    private void OnEnable()
    {
        _moveAction = InputManager.inputActions.Gameplay.Movement;
        _sprintAction = InputManager.inputActions.Gameplay.Sprint;
        _hidingAction = InputManager.inputActions.Gameplay.Sit;
        InputManager.inputActions.Gameplay.RotationRight.started += DoRotationRigth;
        InputManager.inputActions.Gameplay.RotationLeft.started += DoRotationLeft;
        InputManager.inputActions.Gameplay.Interact.started += Interact;
        InputManager.inputActions.InteractionObject.LeaveInteraction.started += Leave;
    }


    private void OnDisable()
    {
        InputManager.inputActions.Gameplay.RotationRight.started -= DoRotationRigth;
        InputManager.inputActions.Gameplay.RotationLeft.started -= DoRotationLeft;
        InputManager.inputActions.Gameplay.Interact.started -= Interact;
        InputManager.inputActions.InteractionObject.LeaveInteraction.started -= Leave;
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        if (_interactableObject != null)
        {
            _interactableObject.GetComponent<IInteractable>().Interact();
            StartCoroutine(HidePlayerVisual());
            gameObject.GetComponent<MouseInputController>().enabled = true;
            InputManager.ToggleActionMap(InputManager.inputActions.InteractionObject);
        }
    }
    private void Leave(InputAction.CallbackContext obj)
    {
        if (_interactableObject != null)
        {
            _interactableObject.GetComponent<IInteractable>().Interact();
            StartCoroutine(ShowPlayerVisual());
            gameObject.GetComponent<MouseInputController>().enabled = false;
            InputManager.ToggleActionMap(InputManager.inputActions.Gameplay);
        }
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

    private void FixedUpdate()
    {
        ApplyMovement();
        _animator.SetFloat("xVelocity", _rigidbody.velocity.magnitude / (_speed * _sprintCoef));
    }

    private void Update()
    {
        FlipSprite(_moveAction.ReadValue<Vector2>().x);
        if (Math.Abs(_playerVisual.transform.rotation.eulerAngles.y - _rotationAlongY) > 0.1f)
        {
            _playerVisual.transform.rotation = Quaternion.Lerp(_playerVisual.transform.rotation, Quaternion.Euler(0, _rotationAlongY, 0), _rotationSpeed * Time.deltaTime);
        }
        else
        {
            _playerVisual.transform.rotation = Quaternion.Euler(0, _rotationAlongY, 0);
        }

        InteractionRay();

        //Debug.Log(_playerVisual.transform.rotation.eulerAngles.y);
    }

    private void ApplyMovement()
    {
        float speed = _speed * (_sprintAction.IsPressed() ? _sprintCoef : 1f);

        if (_hidingAction.IsPressed())
        {
            _animator.SetBool("isHiding", true);
            return;
        }
        else
        {
            _animator.SetBool("isHiding", false);
        }

        float horizontalInput = _moveAction.ReadValue<Vector2>().x;

        if (_rotationAlongY == 0f)
        {
            forceDiraction += horizontalInput * transform.right * speed;
        }
        else if (_rotationAlongY == 180f)
        {
            forceDiraction -= horizontalInput * transform.right * speed;
        }
        else if (_rotationAlongY == 270f)
        {
            forceDiraction += horizontalInput * transform.forward * speed;
        }
        else
        {
            forceDiraction -= horizontalInput * transform.forward * speed;
        }

        _rigidbody.AddForce(forceDiraction, ForceMode.Impulse);
        forceDiraction = Vector3.zero;

        Vector3 horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        if (horizontalVelocity.magnitude > speed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * speed;
            _rigidbody.velocity = new Vector3(limitedVelocity.x, 0f, limitedVelocity.z);
        }
    }

    private void FlipSprite(float input)
    {
        if (input > 0)
        {
            _playerVisual.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            //_playerVisual.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (input < 0)
        {
            _playerVisual.transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
            //_playerVisual.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void InteractionRay()
    {
        Ray ray = new Ray(_playerVisual.transform.position, _playerVisual.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactionDistance, 1<<7) /*&& hit.transform.tag == "CameraChangingObject"*/)
        {
            _interactableObject = hit.collider.gameObject;
            if (_interactableObject != null && _interactableObject != _previousInteractableObject)
            {
                _interactableObject.GetComponent<IInteractable>().OutlineEnable();
                if(_previousInteractableObject != null)
                {
                    _previousInteractableObject.GetComponent<IInteractable>().OutlineDisable();
                }
                _previousInteractableObject = _interactableObject;
            }
        }
        else
        {
            if (_previousInteractableObject != null)
            {
                _previousInteractableObject.GetComponent<IInteractable>().OutlineDisable();
                _previousInteractableObject = null;
            }
            _interactableObject = null;
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }

    private IEnumerator HidePlayerVisual()
    {

        Color startColor = _spriteRenderer.material.color;
        Color finalColor = new Vector4(startColor.r, startColor.g, startColor.b, 0f);
        float t = 0f;
        while (t < 1)
        {
            _spriteRenderer.material.color = Vector4.Lerp(startColor, finalColor, t);
            t += Time.deltaTime / animationDuration;
            yield return null;
        }
        yield break;
    }

    private IEnumerator ShowPlayerVisual()
    {

        Color startColor = _spriteRenderer.material.color;
        Color finalColor = new Vector4(startColor.r, startColor.g, startColor.b, 1f);
        float t = 0f;
        while (t < 1)
        {
            _spriteRenderer.material.color = Vector4.Lerp(startColor, finalColor, t);
            t += Time.deltaTime / animationDuration;
            yield return null;
        }

        yield break;
    }
}
