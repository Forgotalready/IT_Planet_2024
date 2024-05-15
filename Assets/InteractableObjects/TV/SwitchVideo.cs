using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SwitchVideo : MonoBehaviour, IInteractable
{
    [SerializeField] private Outline _outline;
    [SerializeField] private List<VideoClip> _videoClips;
    [SerializeField] private VideoPlayer _tvPlayer;
    private int _videoIndex = -1;

    private void Awake()
    {
        _outline.enabled = false;
    }

    public void Interact()
    {
        _videoIndex += 1;
        if (_videoIndex == _videoClips.Count)
            _videoIndex = 0;
        _tvPlayer.clip = _videoClips[_videoIndex];
        _tvPlayer.Play();
    }

    public void OutlineEnable()
    {
        _outline.enabled = true;
    }

    public void OutlineDisable()
    {
        _outline.enabled = false;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }
}
