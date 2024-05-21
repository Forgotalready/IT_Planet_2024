using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FearLevel : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private AudioSource _audioSource;
    [SerializeField] Transform _enemy;
    [SerializeField] float _startDistance;
    [SerializeField] float _minAudioSpeed;
    [SerializeField] float _maxAudioSpeed;

    [SerializeField] private float _rayDistance;

    private bool _isHidden;
    private bool _inFear = false;


    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _audioSource.Play();

        InputManager.inputActions.InteractionObject.Hide.performed += HidePerformed;
        InputManager.inputActions.InteractionObject.Hide.canceled += HideCanceled;
    }

    private void HideCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isHidden = false;
    }

    private void HidePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        _isHidden = true;
    }

    private void OnDisable()
    {
        _audioSource.Pause();
        InputManager.inputActions.InteractionObject.Hide.performed -= HidePerformed;
        InputManager.inputActions.InteractionObject.Hide.canceled -= HideCanceled;
    }

    private void Update()
    {
        ViewRay();

        if (_inFear)
        {
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, 1, Time.deltaTime);
            _audioSource.pitch = Mathf.Lerp(_audioSource.pitch, _maxAudioSpeed, Time.deltaTime);
        }
        else
        {
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, 0, Time.deltaTime);
            _audioSource.pitch = Mathf.Lerp(_audioSource.pitch, _minAudioSpeed, Time.deltaTime);
        }
    }

    private void ViewRay()
    {
        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit[] hitColliders = Physics.RaycastAll(ray, _rayDistance);

        foreach(var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag == "Enemy" && !_isHidden)
            {
                _inFear = true;
                break;
            }
            if(!_isHidden)
                _inFear = false;
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }

}
