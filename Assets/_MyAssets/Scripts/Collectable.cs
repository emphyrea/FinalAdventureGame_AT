using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{

    public event Action onInteractionEnded;

    public void Interact()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        //send to inventory, check for filled slot or matching item slot with room available
    }
}
