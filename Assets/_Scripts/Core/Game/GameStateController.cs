using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController
{
    // Attributes
    private readonly Stack<GameState> gameStateStack = new();

    // Events
    public event GameStateChangeEvent OnGameStateChange;

    // Constructor
    public GameStateController()
    {
        gameStateStack.Push(GameState.NULL);
        gameStateStack.Push(GameState.RUNNING);
        OnGameStateChange += LogGameStateEvent;
    }


    public void BindDeathEvent(Player player)
    {
        Debug.Log(String.Format("Player is null {0}", player is null));
        player.OnDeathEvent += () =>
        {
            gameStateStack.Push(GameState.OVER);
        };
    }

    // Functions
    public void PushState(GameState gameState)
    {
        gameStateStack.Push(gameState);

        SetState(gameState);
        OnGameStateChange?.Invoke(
            new GameStateChangeArgs(
                StackChangeEventType.PUSH,
                gameStateStack.Count,
                gameState
            )
        );
    }

    public void PopState()
    {
        gameStateStack.Pop();
        GameState gameState = gameStateStack.Peek();

        SetState(gameState);
        OnGameStateChange?.Invoke(
            new GameStateChangeArgs(
                StackChangeEventType.POP,
                gameStateStack.Count,
                gameState
            )
        );
    }

    public void HandleEscape()
    {
        GameState state = GetState();
        if (state == GameState.OVER)
        {
            return;
        }
        switch (state)
        {
            case GameState.RUNNING:
                PushState(GameState.PAUSED);
                return;

            case GameState.CHEAT:
                PopState();
                return;

            case GameState.PAUSED:
                PopState();
                return;
            case GameState.CUTSCENE:
                DialogController.Instance.ProgressCutscene();
                return;
                
            case GameState.MENU:
                PopState();
                return;

            default:
                return;
        }
    }

    // Internal Functions
    private void LogGameStateEvent(GameStateChangeArgs e)
    {
        Debug.Log($"GameState {e.EventType}; Current gamestate is {e.NewGameState}");
    }

    private void SetState(GameState gameState)
    {
        //TODO: Review cutscenes and menu behaviour
        Time.timeScale = gameState switch
        {
            GameState.NULL => 0,
            GameState.PAUSED => 0,
            GameState.CHEAT => 0,
            GameState.RUNNING => 1,
            GameState.CUTSCENE => 0,
            GameState.MENU => 0,
            GameState.FINISH => 0,
            GameState.OVER => 1,
            _ => throw new Exception("Invalid gameState pushed to GameStateController, please refer to enum GameState for valid states"),
        };
        Cursor.lockState = gameState switch
        {
            GameState.NULL => CursorLockMode.None,
            GameState.PAUSED => CursorLockMode.None,
            GameState.CHEAT => CursorLockMode.None,
            GameState.RUNNING => CursorLockMode.Locked,
            GameState.CUTSCENE => CursorLockMode.Locked,
            GameState.MENU => CursorLockMode.None,
            GameState.FINISH => CursorLockMode.None,
            GameState.OVER => CursorLockMode.None,
            _ => throw new Exception("Invalid gameState pushed to GameStateController, please refer to enum GameState for valid states"),
        };
    }

    public GameState GetState()
    {
        return gameStateStack.Peek();
    }
}