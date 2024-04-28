using System;

public abstract class EntityStateController
{
    // Attributes
    protected int state;

    // Set-Getters
    public int State => state;

    // Events
    public event Action<int, int> OnStateChangeEvent;

    // Functions
    protected void InvokeOnStateChanged(int oldState, int newState)
    {
        OnStateChangeEvent?.Invoke(oldState, newState);
    }

    // Abstract Functions
    public abstract int UpdateState();
}
