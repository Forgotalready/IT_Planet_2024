using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    }
    private void Update()
    {
        _interactionText.text = GetDescription();
    }

    private void OnTriggerEnter(Collider collision)
    {
        _interactionUI.SetActive(true);
        _outline.enabled = true;

    }

    private void OnTriggerExit(Collider collision)
    {
        _interactionUI.SetActive(false);
        _outline.enabled = false;
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
