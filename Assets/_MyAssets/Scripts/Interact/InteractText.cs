using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InteractText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactText;
    private void Start()
    {
        interactText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Interactor.isInteract += SetInteractTextActive;
        Interactor.alreadyInteract += SetInteractTextInActive;
    }

    private void SetInteractTextActive()
    {
        interactText.gameObject.SetActive(true);
    }
    private void SetInteractTextInActive()
    {
        interactText.gameObject.SetActive(false);
    } 

    private void OnDisable()
    {
        Interactor.alreadyInteract -= SetInteractTextInActive;
        Interactor.isInteract -= SetInteractTextActive;
    }
}
