using System;

public abstract class EntityStateController
{
    // Attributes
    public int state;

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
