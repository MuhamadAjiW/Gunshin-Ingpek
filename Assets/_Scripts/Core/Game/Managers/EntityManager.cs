using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : BaseObjectManager
{
    // Static instance
    public static EntityManager Instance;

    // Constructor
    protected void Awake()
    {
        Instance = this;
        ManagerName = "Entity Manager";
    }
}
