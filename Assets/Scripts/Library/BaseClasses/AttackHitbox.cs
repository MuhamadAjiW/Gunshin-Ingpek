using System;
using UnityEngine;

public class AttackHitbox : AttackObject{
    // Functions
    void OnTriggerStay(Collider otherCollider){
        Hit(otherCollider);
    }
}
