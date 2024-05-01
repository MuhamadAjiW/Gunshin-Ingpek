using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadGameController : MainMenuScreenController
{

    public new void OnEnable()
    {
        base.OnEnable();

        MainMenuManager.InitializeBackButton(rootElement);
    }
}
