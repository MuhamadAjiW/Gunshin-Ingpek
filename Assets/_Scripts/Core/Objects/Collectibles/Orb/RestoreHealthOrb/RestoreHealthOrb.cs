using UnityEngine;

public class RestoreHealthOrb : Orb
{
    // Static Attributes
    public const string ObjectIdPrefix = "RestoreHealthOrb";

    // Constructor
    protected new void Start()
    {
        base.Start();
        SetIdPrefix(ObjectIdPrefix);
    }

    // Functions
    protected override void OnCollect()
    {
        float prevHealth = collector.Health;

        // Regen by 20% of the collector's max health (not current health)
        collector.InflictHeal(0.2f * collector.MaxHealth);
        Debug.Log(id + ": Health increased from " + prevHealth + " to " + collector.Health);
    }

    protected override void OnTimeout()
    {
        Debug.Log(id + ": Timeout");
    }
}