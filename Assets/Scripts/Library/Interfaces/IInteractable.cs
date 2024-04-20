using System;

public interface IInteractable{
    public void Interact();
    public event Action OnInteractAreaExitEvent;
    public event Action OnInteractAreaEnterEvent;
    public void InvokeOnInteractAreaExitEvent();
    public void InvokeOnInteractAreaEnterEvent();
}