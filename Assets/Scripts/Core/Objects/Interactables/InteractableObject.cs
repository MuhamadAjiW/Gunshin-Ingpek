using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable{
    // Attributes
    public event Action OnInteractAreaExitEvent;
    public event Action OnInteractAreaEnterEvent;

    // Function
    public abstract void Interact();

    public void InvokeOnInteractAreaEnterEvent(){
        OnInteractAreaEnterEvent?.Invoke();
    }

    public void InvokeOnInteractAreaExitEvent(){
        OnInteractAreaExitEvent?.Invoke();
    }
}