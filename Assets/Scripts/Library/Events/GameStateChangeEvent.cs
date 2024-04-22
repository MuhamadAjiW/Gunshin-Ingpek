
public class GameStateChangeArgs : StackChangeEventArgs<GameState>
{
    // Arguments
    public GameState NewGameState => Value;
    
    // Constructor
    public GameStateChangeArgs(StackChangeEventType eventType, int index, GameState NewGameState) : base(eventType, index, NewGameState)
    {
    }
}
public delegate void GameStateChangeEvent(GameStateChangeArgs e);
