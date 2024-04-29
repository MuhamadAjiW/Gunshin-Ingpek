using UnityEngine;

public class RestoreHealthOrb : Orb
{
    // Static Attributes
    public const string ObjectIdPrefix = "RestoreHealthOrb";

    // Attributes
    [SerializeField] float healthMultiplier = 0.2f;

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

        // Regen by healthMultiplier * collector's current health (not max health)
        collector.InflictHeal(healthMultiplier * collector.Health);
        Debug.Log(id + ": Health increased from " + prevHealth + " to " + collector.Health);
    }
}