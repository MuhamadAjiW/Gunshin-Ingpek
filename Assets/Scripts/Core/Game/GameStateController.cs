using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateController {
    // Attributes
    private readonly Stack<GameState> gameStateStack = new();
    event StackChangeEvent<GameState> OnGameStateChange;
    event Action OnPaused;

    // Constructor
    public GameStateController(){
        gameStateStack.Push(GameState.NULL);
    }

    // Functions
    public void PushState(GameState gameState){
        switch(gameState){
            case GameState.PAUSED:
                Time.timeScale = 0;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1;
                break;
            case GameState.CUTSCENE:
                break;
            case GameState.MENU:
                break;
            default:
                throw new Exception("Invalid gameState pushed to GameStateController, please refer to enum GameState for valid states");
        }
        gameStateStack.Push(gameState);
        OnGameStateChange.Invoke(new StackChangeEventArgs<GameState>(
            StackChangeEventType.PUSH,
            gameStateStack.Count,
            gameState
        ));
    }

    public void PopState(){
        gameStateStack.Pop();
        GameState gameState = gameStateStack.Peek();
        switch(gameState){
            case GameState.PAUSED:
                Time.timeScale = 0;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1;
                break;
            case GameState.CUTSCENE:
                break;
            case GameState.MENU:
                break;
        }
        OnGameStateChange.Invoke(new StackChangeEventArgs<GameState>(
            StackChangeEventType.PUSH,
            gameStateStack.Count,
            gameState
        ));
    }

    public void HandleEscape(){
        switch (gameStateStack.Peek()){
            case GameState.RUNNING:
                PushState(GameState.PAUSED);
                OnPaused.Invoke();
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