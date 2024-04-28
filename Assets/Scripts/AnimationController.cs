using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("isWalking", PlayerController.Instance.IsWalking());
        _animator.SetBool("isSprinting", PlayerController.Instance.IsSprinting());
        _animator.SetBool("isSitting", PlayerController.Instance.IsSitting());
    }
}
