using System;

public interface IInteractable
{
    // Events
    public event Action OnInteractAreaExitEvent;
    public event Action OnInteractAreaEnterEvent;

    // Functions
    public void InvokeOnInteractAreaExitEvent();
    public void InvokeOnInteractAreaEnterEvent();
    public void Interact();
}