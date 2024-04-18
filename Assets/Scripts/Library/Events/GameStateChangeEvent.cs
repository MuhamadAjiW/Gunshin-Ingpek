
public class GameStateChangeArgs : StackChangeEventArgs<GameState>{
    public GameState NewGameState => Value;
    public GameStateChangeArgs(StackChangeEventType eventType, int index, GameState NewGameState) : base(eventType, index, NewGameState){}
}
public delegate void GameStateChangeEvent(GameStateChangeArgs e);
