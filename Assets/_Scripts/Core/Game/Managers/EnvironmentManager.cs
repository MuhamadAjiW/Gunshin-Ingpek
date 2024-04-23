using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : BaseObjectManager
{
    // Static instance
    public static EnvironmentManager Instance;

    // Constructor
    protected void Awake()
    {
        Instance = this;
        ManagerName = "Environment Manager";
    }
}
