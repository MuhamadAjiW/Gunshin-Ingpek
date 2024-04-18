using System;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour {
    // Attributes
    protected AttackObjectType bearerType;
    protected IArmed bearer;
    [SerializeField] protected float BaseDamage;
    [SerializeField] protected float KnockbackPower;

    // Constructor
    void Start(){
        bearer = GetComponentInParent<IArmed>();
        if(bearer is Player) bearerType = AttackObjectType.PLAYER;
        else if(bearer is EnemyEntity) bearerType = AttackObjectType.ENEMY;
    }

    // Functions
    public abstract void Attack();
}
