using UnityEngine;

public class ObjectManager : MonoBehaviour{
    // Static Instance
    public static ObjectManager instance;

    // Constructor
    protected void Awake(){
        instance = this;
    }
}