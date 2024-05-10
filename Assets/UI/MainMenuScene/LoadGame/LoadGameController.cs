using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadGameController : MainMenuScreenController
{

    GameSavesContainer GameSavesContainer;

    Button DeleteAllSavesButton;

    public new void OnEnable()
    {
        base.OnEnable();

        MainMenuManager.InitializeBackButton(rootElement);

        GameSavesContainer = rootElement.Query<GameSavesContainer>("GameSavesContainer");

        DeleteAllSavesButton = rootElement.Query<Button>("DeleteAllSavesButton");

        SetupSaves();
        DeleteAllSavesButton.RegisterCallback((ClickEvent evt) =>
        {
            GameSaveManager.Instance.DeleteAllSaves();
            SetupSaves();
        });


    }

    public void SetupSaves()
    {
        GameSavesContainer.LoadSave();
        if (GameSavesContainer.Saves.Count <= 0)
        {
            DeleteAllSavesButton.style.display = DisplayStyle.None;
        }
        else
        {
            DeleteAllSavesButton.style.display = DisplayStyle.Flex;
        }


    }
}
