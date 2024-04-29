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

    public void UpdateState()
    {
        int initialState = state;
 
        state = DetectState();

        if(initialState != state)
        {
            InvokeOnStateChanged(initialState, state);
        }
    }

    // Abstract Functions
    protected abstract int DetectState();
}
