
using System;
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
        Damage = Mathf.Max(initialDamage - initialDamage * Mathf.Pow(distanceTravelled / travelDistance, 4f), 0);
    }
}