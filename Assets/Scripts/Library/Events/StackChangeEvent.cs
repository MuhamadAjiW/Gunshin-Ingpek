using System;

public class StackChangeEventArgs<T> : EventArgs
{
    // Arguments
    public StackChangeEventType EventType { get; }
    public int Index { get; }
    public T Value { get; }

    // Constructor
    public StackChangeEventArgs(StackChangeEventType eventType, int index, T value)
    {
        EventType = eventType;
        Index = index;
        Value = value;
    }
}

public delegate void StackChangeEvent<T>(StackChangeEventArgs<T> e);
