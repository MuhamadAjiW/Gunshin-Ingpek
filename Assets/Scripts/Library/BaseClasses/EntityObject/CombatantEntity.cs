using System;
using UnityEngine;

public class CombatantEntity : DamageableEntity, IArmed{
    // Attributes
    [SerializeField] private float baseDamage;
    private WeaponObject weapon;
    
    // Set-Getters
    public float BaseDamage { 
        get => baseDamage;
        set => baseDamage = value;
    }
    public WeaponObject Weapon { 
        get => weapon; 
        set => weapon = value;
    }
    public Transform Orientation => transform;

    // TODO: Test then decide whether to destroy/disable previous weapon
    public void SwapWeapon(WeaponObject newWeapon){
        Weapon = newWeapon;
    }
}
