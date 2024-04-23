using System;
using UnityEngine;

public interface IKnockback
{
    // Set-Getters
    public Vector3 KnockbackOrigin{get; set;}
    public float KnockbackPower{get; set;}
    
    // Functions
    public void Knockback(IRigid rigidObject);
}
