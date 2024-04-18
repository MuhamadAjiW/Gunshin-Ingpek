using System;

public class GameEventArgs : EventArgs{
    public GameEventType EventType { get; }
    public GameEventArgs(GameEventType eventType){
        EventType = eventType;
    }
}

public delegate void GameEvent(GameEventArgs e);