using UnityEngine;

public class TestCollectible : Collectible{
    // Static attributes
    public static string ObjectIdPrefix = "Player";

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