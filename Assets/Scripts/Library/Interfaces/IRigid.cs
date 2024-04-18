

using UnityEngine;

public interface IRigid{
    public Rigidbody Rigidbody {get;}
    public Collider Collider {get;}
    public Vector3 Position {get;}
    public float KnockbackResistance {get; set;}
}
