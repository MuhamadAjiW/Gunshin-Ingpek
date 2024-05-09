using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuController : InGameUIScreenController
{


    public void Awake()
    {
    }


    public new void OnEnable()
    {
        base.OnEnable();
        Button resumeButton = rootElement.Query<Button>("Resume");
        resumeButton.RegisterCallback((ClickEvent evt) =>
        {
            GameController.stateController.PopState();
        });

        Button saveButton = rootElement.Query<Button>("Resume");
        // Insert condition on which player can save the game
        if (false)
        {
            saveButton.style.display = DisplayStyle.None;
        }
        saveButton.RegisterCallback((ClickEvent evt) =>
        {
            // Change for whatever condition player is allowed to save
            if (true)
            {
                GameSaveManager.Instance?.PersistActiveSave();
            }
        });
    }
}
