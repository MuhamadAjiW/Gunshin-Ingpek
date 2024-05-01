

using UnityEngine;

public interface IRigid
{
    // Set-Getters
    public Rigidbody Rigidbody {get;}
    public Vector3 Position {get;}
    public bool Grounded {get;}
    public float Speed {get; set;}
    public float JumpForce {get; set;}
    public float KnockbackResistance {get; set;}
}
