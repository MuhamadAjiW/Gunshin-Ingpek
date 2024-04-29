using UnityEngine;

public abstract class Orb : Collectible
{
    // Attributes
    protected Player collector;

    // Functions
    protected override void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.transform.parent.gameObject.TryGetComponent(out collector))
        {
            base.OnTriggerEnter(otherCollider);
        }
    }
}