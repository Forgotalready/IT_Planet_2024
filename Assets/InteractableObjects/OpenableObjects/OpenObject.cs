using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObject : MonoBehaviour, IInteractable
{
    private Outline _outline;
    private bool _isOpen = false;
    private Animator _animator;

    [SerializeField] private string _unlockItem;
    [SerializeField] private bool _isBlocked;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;

        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        EntryPoint.BEDA += ApplyItem;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        if (!_isBlocked)
        {
            _isOpen = !_isOpen;

            _animator.SetBool("Open", _isOpen);
        }
        else
        {
            _animator.SetBool("Blocked", true);
        }
    }

    public void OutlineDisable()
    {
        _outline.enabled = false;
    }

    public void OutlineEnable()
    {
        _outline.enabled = true;
    }

    private void ApplyItem(string itemName)
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<MouseInputController>().enabled)
        {
            Debug.Log("1" + itemName);
            Debug.Log("2" + _unlockItem);
            if (_unlockItem == itemName)
            {
                EntryPoint.Instance.RemoveItems(itemName, 1);
                _isBlocked = false;
                _isOpen = true;
                _animator.SetBool("Blocked", _isBlocked);
                _animator.SetBool("Open", _isOpen);
            }
        }
    }
}
