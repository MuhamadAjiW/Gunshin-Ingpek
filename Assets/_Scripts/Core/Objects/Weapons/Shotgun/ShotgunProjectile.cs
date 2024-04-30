
using UnityEngine;

public class ShotgunProjectile : ProjectileObject
{
    // Attributes
    public float initialDamage;

    // Constructor
    protected new void Start()
    {
        base.Start();
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
        Debug.Log(((travelDistance - distanceTravelled) / travelDistance));
        Damage = initialDamage * ((travelDistance - distanceTravelled) / travelDistance);
    }
}