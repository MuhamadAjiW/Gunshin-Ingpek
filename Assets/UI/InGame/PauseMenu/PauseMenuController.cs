using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        VisualElement resumeButton = rootElement.Query("Resume");
        resumeButton.RegisterCallback((ClickEvent evt) =>
        {
            GameController.stateController.PopState();
        });
    }
}
