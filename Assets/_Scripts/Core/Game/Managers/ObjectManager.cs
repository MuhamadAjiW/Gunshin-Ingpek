using UnityEngine;

public class ObjectManager : BaseObjectManager
{
    // Static Instance
    public static ObjectManager Instance;

    // Constructor
    protected void Awake()
    {
        Instance = this;
        ManagerName = "Object Manager";
    }
}