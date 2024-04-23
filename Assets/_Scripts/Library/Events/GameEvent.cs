using System;

public class GameEventArgs : EventArgs
{
    // Arguments
    public GameEventType EventType { get; }
    
    // Constructor
    public GameEventArgs(GameEventType eventType)
    {
        EventType = eventType;
    }
}

public delegate void GameEvent(GameEventArgs e);
