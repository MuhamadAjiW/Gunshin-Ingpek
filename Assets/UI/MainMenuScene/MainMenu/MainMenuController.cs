using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MainMenuScreenController
{

    private VisualElement MaxSavesContainer;
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

        Button exitButton = rootElement.Query<Button>("exit-button").First();
        exitButton.RegisterCallback(BlockCallbackInTransition(ExitCallback));

        // Setup max saves reached

        Debug.Log("MainMenu Controller Enable");

        MaxSavesContainer = rootElement.Query<VisualElement>("MaxSavesContainer");

        Debug.Log(MaxSavesContainer.childCount);

        Button yesButton = rootElement.Query<Button>("Yes");
        Button cancelButton = rootElement.Query<Button>("Cancel");

        yesButton.RegisterCallback((ClickEvent evt) =>
        {
            GameSaveManager.Instance?.OverrideSave();
            ScenesManager.Instance.LoadNewGame();
        });

        cancelButton.RegisterCallback((ClickEvent evt) =>
        {
            UIManagement.ToggleElementVisible(MaxSavesContainer, false);
        });

        UIManagement.ToggleElementVisible(MaxSavesContainer, false);

        GameSaveManager.Instance.LoadStatistics();


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
        Debug.Assert(GameSaveManager.Instance != null);
        GameSaveManager.GameSaveResult newSaveResult = GameSaveManager.Instance.NewSave();
        if (newSaveResult == GameSaveManager.GameSaveResult.SUCCESS)
        {
            ScenesManager.Instance.LoadNewGame();
            return;
        }

        UIManagement.ToggleElementVisible(MaxSavesContainer, true);

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
