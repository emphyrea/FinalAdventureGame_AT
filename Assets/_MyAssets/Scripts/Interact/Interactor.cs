using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    public Transform interactorSource;
    public float interactRange;

    public delegate void isInteracting();
    public static event isInteracting isInteract;
    public static event isInteracting alreadyInteract;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interactableObj))
            {
                isInteract?.Invoke();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    alreadyInteract?.Invoke();
                    interactableObj.Interact();
                }
            }
        }

    }


}
