using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateController{
    // Attributes
    private readonly Stack<GameState> gameStateStack = new();
    event GameStateChangeEvent OnGameStateChange;
    event Action OnPausedEvent;

    // Constructor
    public GameStateController(){
        gameStateStack.Push(GameState.NULL);
        gameStateStack.Push(GameState.RUNNING);
        OnGameStateChange += LogGameStateStack;
    }

    // Functions
    private void LogGameStateStack(GameStateChangeArgs e){
        Debug.Log(string.Format("GameState {0}; Current gamestate is {1}", e.EventType, e.NewGameState));
    }

    public void PushState(GameState gameState){
        //TODO: Review cutscenes and menu behaviour
        Time.timeScale = gameState switch{
            GameState.PAUSED => 0,
            GameState.RUNNING => 1,
            GameState.CUTSCENE => 0,
            GameState.MENU => 0,
            _ => throw new Exception("Invalid gameState pushed to GameStateController, please refer to enum GameState for valid states"),
        };

        gameStateStack.Push(gameState);
        OnGameStateChange?.Invoke(new GameStateChangeArgs(
            StackChangeEventType.PUSH,
            gameStateStack.Count,
            gameState
        ));
    }

    public void PopState(){
        gameStateStack.Pop();
        GameState gameState = gameStateStack.Peek();
        //TODO: Review cutscenes behaviour
        Time.timeScale = gameState switch{
            GameState.PAUSED => 0,
            GameState.RUNNING => 1,
            GameState.CUTSCENE => 0,
            GameState.MENU => 0,
            _ => throw new Exception("Invalid gameState popped to GameStateController, please refer to enum GameState for valid states"),
        };

        OnGameStateChange?.Invoke(new GameStateChangeArgs(
            StackChangeEventType.POP,
            gameStateStack.Count,
            gameState
        ));
    }

    public void HandleEscape(){
        switch (gameStateStack.Peek()){
            case GameState.RUNNING:
                PushState(GameState.PAUSED);
                OnPausedEvent?.Invoke();
                return;

            case GameState.PAUSED:
            case GameState.CUTSCENE:
            case GameState.MENU:
                PopState();
                return;
            
            default:
                return;
        }
    }
}
