using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _sprintCoef = 1.5f;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 10f;

    private Rigidbody _rigidbody;
    private PlayerInputHandler _playerInputHandler;
    private float _horizontalInput;
    private bool _isGrounded;
    private bool _shouldJump;

    private bool _isWalking = false;
    private bool _isSprinting = false;

    private bool _isRotate = false;
    private bool _switchMap = false;

    private PlayerInput _playerInput;

    [SerializeField] private CinemachineVirtualCamera _camera;

    private IInteractable _interactableObject;
    private Transform _interactableObjectTransform;
    private string _interactableObjectsTag;

    private void Awake()
    {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _playerInputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        _horizontalInput = _playerInputHandler.moveInput.x;
        _shouldJump = _playerInputHandler.jumpTriggered && _isGrounded;

        if (_horizontalInput != 0)
        {
            FlipSprite(_horizontalInput);
        }

        if (_playerInputHandler.interactTriggered)
        {
            IsInteract();
        }

        if (_playerInputHandler.rotateTriggered)
        {
            RotatePlayer();
        }
        if (_isRotate)
        {
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 10f);
        }
        else
        {
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 10f);
        }
    }

    private void OnEnable()
    {
        PlayerInputHandler.OnSwitch += SwitchMap;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnSwitch -= SwitchMap;
    }

    private void SwitchMap(string str)
    {
        if(_interactableObjectsTag == "Interactable")
        {
            _switchMap = !_switchMap;
            if (_switchMap)
            {
                _playerInput.SwitchCurrentActionMap(str);
            }
            else
            {
                _playerInput.SwitchCurrentActionMap("Player");
            }
            CameraFollowChange();
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        if (_shouldJump)
        {
            ApplyJump();
        }
    }

    private void ApplyMovement()
    {
        float speed = _moveSpeed * (_playerInputHandler.sprintValue > 0 ? _sprintCoef : 1f);
        if (_isRotate)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, _horizontalInput * speed);
        }
        else
        {
            _rigidbody.velocity = new Vector3(_horizontalInput * speed, _rigidbody.velocity.y, _rigidbody.velocity.z);
        }
        if (Mathf.Abs(_horizontalInput) > 0)
            _isWalking = true;
        else
            _isWalking = false;

        if (_playerInputHandler.sprintValue > 0)
            _isSprinting = true;
        else
            _isSprinting = false;
    }

    void ApplyJump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isGrounded = false;
        _shouldJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    void RotatePlayer()
    {
        _playerInputHandler.rotateTriggered = false;
        _isRotate = !_isRotate;
    }

    private void FlipSprite(float input)
    {
        if (input > 0)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        else if (input < 0)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }

    private void IsInteract()
    {
        _playerInputHandler.interactTriggered = false;
        if (_interactableObject != null)
        {
            _interactableObject.Interact();
        }
    }
    private void CameraFollowChange()
    {
        if (_switchMap)
        {
            _camera.Follow = _interactableObjectTransform;
            _camera.m_Lens.FieldOfView = 50f;
        }
        else
        {
            _camera.Follow = transform;
            _camera.m_Lens.FieldOfView = 100f;
        }
    }


    public bool IsWalking()
    {
        return _isWalking;
    }

    public bool IsSprinting()
    {
        return _isSprinting;
    }

    private void OnTriggerEnter(Collider collision)
    {
        _interactableObject = collision.GetComponent<IInteractable>();
        _interactableObjectTransform = collision.gameObject.transform;
        _interactableObjectsTag = collision.gameObject.tag;
    }

    private void OnTriggerExit(Collider collision)
    {
        _interactableObject = null;
        _interactableObjectTransform = null;
    }
}
