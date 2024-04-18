using System;
using UnityEngine;

public class AttackHitbox : AttackObject{
    void OnTriggerStay(Collider otherCollider){
        Debug.Log(string.Format("Collision in hitbox of {0} by {1}", transform.name, otherCollider.transform.name));
        Hit(otherCollider);
    }
}