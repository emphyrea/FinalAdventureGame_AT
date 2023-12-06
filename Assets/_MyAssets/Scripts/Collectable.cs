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
        Debug.Log("added");
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        onInteractionEnded?.Invoke();
    }

}
