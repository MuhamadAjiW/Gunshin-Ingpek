
public class GameStateChangeArgs : StackChangeEventArgs<GameState>{
    public GameStateChangeArgs(StackChangeEventType eventType, int index, GameState value) : base(eventType, index, value){}
}
public delegate void GameStateChangeEvent(GameStateChangeArgs e);