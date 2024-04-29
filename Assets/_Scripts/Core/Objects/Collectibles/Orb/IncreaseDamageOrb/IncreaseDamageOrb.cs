using UnityEngine;

public class IncreaseDamageOrb : Orb
{
    // Static Attributes
    public const string ObjectIdPrefix = "IncreaseDamageOrb";
    [SerializeField] float baseDamageMultiplier = 0.1f;

    // Constructor
    protected new void Start()
    {
        base.Start();
        SetIdPrefix(ObjectIdPrefix);
    }

    // Functions
    protected override void OnCollect()
    {
        base.OnCollect();
        collector.ActivateIncDamageOrb(baseDamageMultiplier);
    }

    protected override void OnTriggerEnter(Collider otherCollider)
    {
        if
        (
            otherCollider.transform.parent.gameObject.TryGetComponent(out collector) &&
            collector.incDamageOrbCount < collector.maxIncDamageOrbCount
        )
        {
            base.OnTriggerEnter(otherCollider);
        }
    }
}