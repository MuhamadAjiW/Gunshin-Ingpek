using UnityEngine;

public class IncreaseSpeedOrb : Orb
{
    // Static Attributes
    public const string ObjectIdPrefix = "IncreaseSpeedOrb";

    // Attributes
    [SerializeField] float speedMultiplier = 0.2f;
    [SerializeField] float duration = 15f;

    // Constructor
    protected new void Start()
    {
        base.Start();
        SetIdPrefix(ObjectIdPrefix);
    }

    // Functions
    protected override void OnCollect()
    {
        float prevBaseSpeed = collector.BaseSpeed;
        collector.ActivateIncSpeedOrb(duration, 1 + speedMultiplier);
        Debug.Log(id + ": Base speed increased from " + prevBaseSpeed + " to " + collector.BaseSpeed);
    }
}