using System;
using UnityEngine;

public interface IKnockback{
    public Vector3 KnockbackOrigin{get; set;}
    public float KnockbackPower{get; set;}
    public void Knockback(IRigid rigidObject);
}
