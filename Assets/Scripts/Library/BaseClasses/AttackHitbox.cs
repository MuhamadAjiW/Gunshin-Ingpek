using System;
using UnityEngine;

public class AttackHitbox : AttackObject{
    // Functions
    protected void OnTriggerStay(Collider otherCollider){
        Hit(otherCollider);
    }
}
