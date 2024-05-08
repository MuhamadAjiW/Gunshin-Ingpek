using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MainMenuScreenController
{
    public new void OnEnable()
    {
        base.OnEnable();

        // Setup click functionalities of the buttons
        Button startGameButton = rootElement.Query<Button>("start-game-button").First();
        startGameButton.RegisterCallback(BlockCallbackInTransition(NewGameCallback));

        Button loadGameButton = rootElement.Query<Button>("load-game-button").First();
        loadGameButton.RegisterCallback(BlockCallbackInTransition(LoadGameCallback));

        Button settingsButton = rootElement.Query<Button>("settings-button").First();
        settingsButton.RegisterCallback(BlockCallbackInTransition(SettingsCallback));

        Button gameStaticsticsDisplayButton = rootElement.Query<Button>("game-statistics-button").First();
        gameStaticsticsDisplayButton.RegisterCallback(BlockCallbackInTransition(GameStatisticsDisplayCallback));

        Button exitButton = rootElement.Query<Button>("exit-game-button").First();
        exitButton.RegisterCallback(BlockCallbackInTransition(ExitCallback));
    }

    private EventCallback<ClickEvent> BlockCallbackInTransition(EventCallback<ClickEvent> callback)
    {
        return (ClickEvent evt) =>
        {
            if (MainMenuManager.MainMenuTransitionInProgress)
            {
                return;
            }
            callback(evt);
        };

    }

    private void NewGameCallback(ClickEvent evt)
    {
        Debug.Assert(GameSaveManager.Instance is not null);
        GameSaveManager.GameSaveResult newSaveResult = GameSaveManager.Instance.NewSave();
        if (newSaveResult == GameSaveManager.GameSaveResult.SUCCESS)
        {
            ScenesManager.Instance.LoadNewGame();
            return;
        }

        MainMenuManager.DisplayUIDocument("MaximumSavesReached");
    }

    private void LoadGameCallback(ClickEvent evt)
    {
        MainMenuManager.DisplayUIDocument("LoadGame", true);
    }

    private void SettingsCallback(ClickEvent evt)
    {
        MainMenuManager.DisplayUIDocument("Settings", true);
    }
    private void GameStatisticsDisplayCallback(ClickEvent evt)
    {
        MainMenuManager.DisplayUIDocument("GameStatisticsDisplay", true);
    }

    private void ExitCallback(ClickEvent evt)
    {
    }
}
