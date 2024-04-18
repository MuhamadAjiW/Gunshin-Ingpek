using System;

public class StackChangeEventArgs<T> : EventArgs{
    public StackChangeEventType EventType { get; }
    public int Index { get; }
    public T Value { get; }

    public StackChangeEventArgs(StackChangeEventType eventType, int index, T value){
        EventType = eventType;
        Index = index;
        Value = value;
    }
}

public delegate void StackChangeEvent<T>(StackChangeEventArgs<T> e);
