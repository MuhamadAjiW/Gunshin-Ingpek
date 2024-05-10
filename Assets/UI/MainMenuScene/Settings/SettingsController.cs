using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsController : MainMenuScreenController
{

    SettingsContainer settingsContainer;


    public new void OnEnable()
    {
        base.OnEnable();

        MainMenuManager.InitializeBackButton(rootElement);

        settingsContainer = rootElement.Query<SettingsContainer>("SettingsContainer");

        settingsContainer.Setup();



    }
}
