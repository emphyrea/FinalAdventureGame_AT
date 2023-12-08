using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    [SerializeField] Item item;
    public event Action onInteractionEnded;

    public void Interact()
    {
        Inventory.Instance.AddItem(item);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        onInteractionEnded?.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Inventory.Instance.AddItem(item);
            Destroy(this.gameObject);
        }
    }
}
