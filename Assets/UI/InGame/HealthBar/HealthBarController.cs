using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : InGameUIScreenController
{


    public new void OnEnable()
    {
        base.OnEnable();
        rootElement.Q<HealthBar>().dataSource = player;
    }
}
