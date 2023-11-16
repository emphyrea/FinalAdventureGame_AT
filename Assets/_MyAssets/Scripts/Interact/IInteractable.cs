using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public event Action onInteractionEnded;
    public void Interact();
}
