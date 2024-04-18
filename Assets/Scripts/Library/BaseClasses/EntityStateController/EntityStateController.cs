using System;

public abstract class EntityStateController{
    // Attributes
    public int state;
    public event Action OnStateChangeEvent;

    // Functions
    protected void InvokeOnStateChanged(){
        OnStateChangeEvent?.Invoke();
    }

    public abstract int UpdateState();
}
