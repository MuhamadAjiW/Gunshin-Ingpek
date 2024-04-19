using UnityEngine;

public class ObjectManager : BaseObjectManager{
    // Static Instance
    public static ObjectManager instance;

    // Constructor
    protected void Awake(){
        instance = this;
        ManagerName = "Object Manager";
    }
}