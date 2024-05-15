using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVOn : MonoBehaviour, IInteractable
{
    private Outline _outline;
    [SerializeField] private List<GameObject> _objects;

    private bool _screenIsOn = false;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        foreach (GameObject obj in _objects)
        {
            obj.SetActive(false);
        }

    }

    public void Interact()
    {
        _screenIsOn = !_screenIsOn;
        foreach (GameObject obj in _objects) 
        {
            obj.SetActive(_screenIsOn);
        }
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
