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
        Debug.Log(loadGameButton.ToString());
        loadGameButton.RegisterCallback<ClickEvent>(LoadGameCallback);

        Button settingsButton = rootElement.Query<Button>("settings-button").First();
        settingsButton.RegisterCallback<ClickEvent>(SettingsCallback);

        Button gameStaticsticsDisplayButton = rootElement.Query<Button>("game-statistics-display-button").First();
        gameStaticsticsDisplayButton.RegisterCallback<ClickEvent>(GameStatisticsDisplayCallback);

        Button exitButton = rootElement.Query<Button>("exit-button").First();
        exitButton.RegisterCallback<ClickEvent>(ExitCallback);

    }

    private void NewGameCallback(ClickEvent evt)
    {
    }

    private void LoadGameCallback(ClickEvent evt)
    {
        Debug.Log("Load game clicked");
        MainMenuManager.DisplayUIDocument("LoadGame");
    }

    private void SettingsCallback(ClickEvent evt)
    {
    }

    private void GameStatisticsDisplayCallback(ClickEvent evt)
    {
    }

    private void ExitCallback(ClickEvent evt)
    {
    }
}
