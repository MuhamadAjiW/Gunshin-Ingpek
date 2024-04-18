using System;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour {
    // Attributes
    [SerializeField] private float baseDamage;
    public float BaseDamage { 
        get => baseDamage;
        set => baseDamage = value;
    }

    // Functions
    public abstract void Attack();
}
