using _Scripts.Core.Game.Data;
using UnityEngine;

public abstract class Orb : Collectible
{
    // Functions
    protected override void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.transform.parent.gameObject.TryGetComponent<Player>(out _))
        {
            base.OnTriggerEnter(otherCollider);
        }
    }

    protected override void OnCollect()
    {
        GameStatistics.Instance.AddOrbsCollected();
        Debug.Log(id + ": Collected. Current orbs collected: " + GameStatistics.Instance.OrbsCollected);
    }

    protected override void OnTimeout()
    {
        Debug.Log(id + ": Timeout");
    }
}