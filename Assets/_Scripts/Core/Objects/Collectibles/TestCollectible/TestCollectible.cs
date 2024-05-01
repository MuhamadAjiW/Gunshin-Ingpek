using UnityEngine;

public class TestCollectible : Collectible
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "TestCollectible";

    // Constructor
    protected new void Start()
    {
        base.Start();
        SetIdPrefix(OBJECT_ID_PREFIX);
    }

    // Functions
    protected override void OnCollect()
    {
        Debug.Log("Test collectible collected");
    }

    protected override void OnTimeout()
    {
        Debug.Log("Test collectible timeout");
    }
}