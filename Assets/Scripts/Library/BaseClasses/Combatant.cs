using System;
using UnityEngine;

public class Combatant : DamageableEntity, IArmed{
    [SerializeField] private float baseDamage;
    private WeaponObject weapon;
    public float BaseDamage { 
        get => baseDamage;
        set => baseDamage = value;
    }
    public WeaponObject Weapon { 
        get => weapon; 
        set => Weapon = value;
    }
}