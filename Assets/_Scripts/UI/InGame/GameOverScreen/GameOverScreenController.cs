using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScreenController : InGameUIScreenController
{

    public CameraManager CameraManager;

    public GameStatisticsContainer gameStatisticsContainer;


    public void Awake()
    {
    }


    public new void Start()
    {
        base.Start();

        Button returnToMainMenu = rootElement.Query<Button>("ReturnToMainMenu");
        returnToMainMenu.RegisterCallback((ClickEvent evt) =>
        {
            ScenesManager.Instance.LoadMainMenu();
        });

        gameStatisticsContainer = rootElement.Query<GameStatisticsContainer>();

        gameStatisticsContainer.ListenToChange();

    }
}
