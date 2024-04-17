using System;

public abstract class StateController{
    // Attributes
    public int state;
    public event Action OnStateChange;

    // Functions
    protected void InvokeOnStateChanged(){
        OnStateChange?.Invoke();
    }

    public abstract int DetectState();
}