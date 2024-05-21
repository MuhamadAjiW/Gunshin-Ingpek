using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MaximumSavesReached : MainMenuScreenController
{

    public new void Start()
    {
        base.Start();
        InnerContainer.AddToClassList(UIManagement.USSAnimationClasses.Hidden);

        Button yesButton = rootElement.Query<Button>("Yes");
        Button cancelButton = rootElement.Query<Button>("Cancel");

        yesButton.RegisterCallback((ClickEvent evt) =>
        {
            GameSaveManager.Instance?.OverrideSave();
            ScenesManager.Instance.LoadNewGame();
        });

        cancelButton.RegisterCallback((ClickEvent evt) =>
        {
            InnerContainer.AddToClassList(UIManagement.USSAnimationClasses.Hidden);
            InnerContainer.RemoveFromClassList(UIManagement.USSAnimationClasses.Flex);
        });

    }
}
