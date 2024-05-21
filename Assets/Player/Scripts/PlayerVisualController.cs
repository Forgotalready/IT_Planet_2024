using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [Header("Show/Hide Visual Settings")]
    [SerializeField] private float animationDuration = 1f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer =GetComponent<SpriteRenderer>();
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
