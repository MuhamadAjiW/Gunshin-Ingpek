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
        collector.ActivateRestoreHealthOrb(healthMultiplier);
    }
}