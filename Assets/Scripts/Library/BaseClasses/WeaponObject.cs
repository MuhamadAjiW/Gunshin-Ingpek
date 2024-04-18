using System;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour {
    // Attributes
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseKnockbackPower;
    public float BaseDamage { 
        get => baseDamage;
        set => baseDamage = value;
    }
    public float BaseKnockbackPower { 
        get => baseKnockbackPower;
        set => BaseKnockbackPower = value;
    }

    // Functions
    public abstract void Attack();
}
