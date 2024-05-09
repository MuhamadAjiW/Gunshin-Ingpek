using System;
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
        PetCounter petCounter = rootElement.Q<PetCounter>();

        player.OnCompanionAggregationChange += () =>
        {
            Debug.Log("Companion aggregation in health bar controller changed");
            Debug.Log(String.Format("[Health Bar controller] Companion Aggregation Count: {0}", player.CompanionAggregation.Keys.Count));
            petCounter.CompanionAggregation = player.CompanionAggregation;
        };
    }
}
