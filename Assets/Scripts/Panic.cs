using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using System;

public class Panic : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip levelPanica;
    [SerializeField] AudioClip endSound;
    [SerializeField] private GameObject panel;
    private Image img;
    [SerializeField] private float darkCoef = 1;

    private bool isPanicFirst = true;



    private void Start()
    {
        img = panel.GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.01f;
        audioSource.Play();
        audioSource.clip = levelPanica;
        StartCoroutine(UpLevelVolume());
    }

    IEnumerator UpLevelVolume()
    {
        yield return new WaitForSecondsRealtime(1f);

        audioSource.volume += 0.02f;
        if (!PlayerInputHandler.Instance.sitTrigger)
        {
            if (audioSource.volume == 1f)
            {
                audioSource.clip = endSound;
                audioSource.Play();
                PlayerController.Instance.enabled = false;
                yield return new WaitForSeconds(6f);
                Exit();
            }
            if (isPanicFirst && audioSource.volume > 0.6f)
            {
                
                EventBus.onPanicHight?.Invoke();
            }
            img.color = new Color(0, 0, 0, 0 + audioSource.volume * darkCoef);
            StartCoroutine(UpLevelVolume());
        }
        else
        {
            StartCoroutine(DownLevelVolume());
        }

    }

    IEnumerator DownLevelVolume()
    {
        yield return new WaitForSecondsRealtime(1f);
        audioSource.volume -= 0.08f;
        if (PlayerInputHandler.Instance.sitTrigger)
        {
            if (isPanicFirst && audioSource.volume < 0.1f)
            {
                isPanicFirst = false;
                EventBus.onPanicLow?.Invoke();
            }
            img.color = new Color(0, 0, 0, 0 + audioSource.volume * darkCoef);
            StartCoroutine(DownLevelVolume());
        }
        else
        {
            StartCoroutine(UpLevelVolume());
        }

    }

    public void Exit()

    {



#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;



#elif UNITY_STANDALONE

            Application.Quit(); //выход из игры

#endif

    }
}
