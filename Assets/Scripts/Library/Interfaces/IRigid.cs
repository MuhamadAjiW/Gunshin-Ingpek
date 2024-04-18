

using UnityEngine;

public interface IRigid{
    public Rigidbody Rigidbody {get;}
    public Collider Collider {get;}
    public Vector3 Position {get;}
    public float BaseSpeed {get; set;}
    public float KnockbackResistance {get; set;}
}
