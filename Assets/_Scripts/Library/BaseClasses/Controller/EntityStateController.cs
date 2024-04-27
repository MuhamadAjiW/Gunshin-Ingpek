using System;

public abstract class EntityStateController
{
    // Attributes
    protected int state;

    // Set-Getters
    public int State => state;

    // Events
    public event Action OnStateChangeEvent;

    // Functions
    protected void InvokeOnStateChanged()
    {
        OnStateChangeEvent?.Invoke();
    }

    // Abstract Functions
    public abstract int UpdateState();
}
