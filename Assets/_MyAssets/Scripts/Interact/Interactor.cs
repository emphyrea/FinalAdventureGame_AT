using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interactor;

public class Interactor : MonoBehaviour
{

    public Transform interactorSource;
    public float interactRange;

    public delegate void isInteracting();
    public static event isInteracting isInteract;
    public static event isInteracting alreadyInteract;
    bool interacting = false;  
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(interacting)
        {
            return;
        }

        ray = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interactableObj))
            {
                isInteract?.Invoke();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interacting = true;
                    interactableObj.onInteractionEnded += RestInteraction;
                    alreadyInteract?.Invoke();
                    interactableObj.Interact();
                }
            }
        }

    }

    private void RestInteraction()
    {
        interacting = false;
    }
}
