using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameStatisticsDisplayController : MainMenuScreenController
{
    public new void Start()
    {
        base.Start();

        MainMenuManager.InitializeBackButton(rootElement);

        GameStatisticsContainer gameStatisticsContainer = rootElement.Query<GameStatisticsContainer>("GameStatisticsContainer");

        GameSaveManager.Instance.LoadStatistics();
        gameStatisticsContainer.LoadStatistics();

        gameStatisticsContainer.ListenToChange();

    }
}
