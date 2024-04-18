using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateController {
    // Attributes
    private readonly Stack<GameState> gameStateStack = new();
    event GameStateChangeEvent OnGameStateChange;
    event Action OnPaused;

    // Constructor
    public GameStateController(){
        gameStateStack.Push(GameState.NULL);
        gameStateStack.Push(GameState.RUNNING);
        OnGameStateChange += LogGameStateStack;
    }

    // Functions
    private void LogGameStateStack(StackChangeEventArgs<GameState> e){
        Debug.Log(e.EventType);
        Debug.Log(gameStateStack);
    }

    public void PushState(GameState gameState){
        switch(gameState){
            case GameState.PAUSED:
                Time.timeScale = 0;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1;
                break;
            //TODO: Review cutscenes and menu behaviour
            case GameState.CUTSCENE:
                Time.timeScale = 0;
                break;
            case GameState.MENU:
                Time.timeScale = 0;
                break;
            default:
                throw new Exception("Invalid gameState pushed to GameStateController, please refer to enum GameState for valid states");
        }
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
        switch(gameState){
            case GameState.PAUSED:
                Time.timeScale = 0;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1;
                break;
            //TODO: Review cutscenes behaviour
            case GameState.CUTSCENE:
                Time.timeScale = 0;
                break;
            case GameState.MENU:
                Time.timeScale = 0;
                break;
        }
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
                OnPaused?.Invoke();
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