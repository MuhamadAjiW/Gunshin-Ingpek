using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScreenController : InGameUIScreenController
{

    public CameraManager CameraManager;


    public void Awake()
    {
    }


    public new void OnEnable()
    {
        base.OnEnable();

        Button returnToMainMenu = rootElement.Query<Button>("ReturnToMainMenu");
        returnToMainMenu.RegisterCallback((ClickEvent evt) =>
        {
            ScenesManager.Instance.LoadMainMenu();
        });
    }
}
