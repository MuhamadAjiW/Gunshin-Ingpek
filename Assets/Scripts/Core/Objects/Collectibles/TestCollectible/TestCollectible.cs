using UnityEngine;

public class TestCollectible : Collectible{
    protected override void OnCollect(){
        Debug.Log("Test collectible collected");
    }

    protected override void OnTimeout(){
        Debug.Log("Test collectible timeout");
    }
}