using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class LightSwitsh : MonoBehaviour, IInteractable
{
    [SerializeField] private Light[] _lights;
    [SerializeField] private GameObject _interactionUI;
    [SerializeField] private TextMeshProUGUI _interactionText;
    private Outline _outline;
    private bool isOn = true;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0f;
    }
    private void Update()
    {
        _interactionText.text = GetDescription();
    }

    public void OutlineEnable()
    {
        _outline.OutlineWidth = 2f;
    }
    public void OutlineDisable()
    {
        _outline.OutlineWidth = 0f;
    }

    public void Interact()
    {
        isOn = !isOn;
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].enabled = isOn;
        }
    }

    public string GetDescription()
    {
        if (isOn) return "Выкл";
        return "Вкл";
    }
}
