using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] public UIDocument MainMenuUIDocument;
    [SerializeField] public MainMenuManager MainMenuManager;


    private VisualElement rootElement;
    public void OnEnable()
    {
        rootElement = MainMenuUIDocument.rootVisualElement;

        // Setup click functionalities of the buttons
        Button startGameButton = rootElement.Query<Button>("start-game-button").First();
        startGameButton.RegisterCallback<ClickEvent>(NewGameCallback);

        Button loadGameButton = rootElement.Query<Button>("load-game-button").First();
        loadGameButton.RegisterCallback<ClickEvent>(LoadGameCallback);

        Button settingsButton = rootElement.Query<Button>("settings-button").First();
        settingsButton.RegisterCallback<ClickEvent>(SettingsCallback);

        Button gameStaticsticsDisplayButton = rootElement.Query<Button>("game-statistics-button").First();
        gameStaticsticsDisplayButton.RegisterCallback<ClickEvent>(GameStatisticsDisplayCallback);

        Button exitButton = rootElement.Query<Button>("exit-game-button").First();
        exitButton.RegisterCallback<ClickEvent>(ExitCallback);

    }

    private void NewGameCallback(ClickEvent evt)
    {
    }

    private void LoadGameCallback(ClickEvent evt)
    {
        MainMenuManager.DisplayUIDocument("LoadGame");
    }

    private void SettingsCallback(ClickEvent evt)
    {
        MainMenuManager.DisplayUIDocument("Settings");
    }

    private void GameStatisticsDisplayCallback(ClickEvent evt)
    {
        MainMenuManager.DisplayUIDocument("GameStatisticsDisplay");
    }

    private void ExitCallback(ClickEvent evt)
    {
    }
}
