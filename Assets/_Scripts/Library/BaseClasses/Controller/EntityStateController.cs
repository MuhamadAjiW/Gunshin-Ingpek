using System;

public abstract class EntityStateController
{
    // Attributes
    protected int state;

    // Set-Getters
    public int State => state;

    // Events
    public event Action<int> OnStateChangeEvent;

    // Functions
    protected void InvokeOnStateChanged(int oldState)
    {
        OnStateChangeEvent?.Invoke(oldState);
    }

    // Abstract Functions
    public abstract int UpdateState();
}
