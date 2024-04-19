using UnityEngine;

public class TestCollectible : Collectible{
    // Static attributes
    public static string ObjectIdPrefix = "TestCollectible";

    // Constructor
    protected new void Start(){
        base.Start();
        SetIdPrefix(ObjectIdPrefix);
    }

    // Functions
    protected override void OnCollect(){
        Debug.Log("Test collectible collected");
    }

    protected override void OnTimeout(){
        Debug.Log("Test collectible timeout");
    }
}