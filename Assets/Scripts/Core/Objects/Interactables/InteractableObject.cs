using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    // Events
    public event Action OnInteractAreaExitEvent;
    public event Action OnInteractAreaEnterEvent;

    // Function

    public void InvokeOnInteractAreaEnterEvent()
    {
        OnInteractAreaEnterEvent?.Invoke();
    }

    public void InvokeOnInteractAreaExitEvent()
    {
        OnInteractAreaExitEvent?.Invoke();
    }

    // Abstract Functions
    public abstract void Interact();
}