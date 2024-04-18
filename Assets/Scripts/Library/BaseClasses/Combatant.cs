using System;
using UnityEngine;

public class Combatant : DamageableEntity, IArmed{
    // Attributes
    [SerializeField] private float baseDamage;
    private WeaponObject weapon;
    public float BaseDamage { 
        get => baseDamage;
        set => baseDamage = value;
    }
    public WeaponObject Weapon { 
        get => weapon; 
        set => weapon = value;
    }

    // TODO: Test then decide whether to destroy/disable previous weapon
    public void SwapWeapon(WeaponObject newWeapon){
        Weapon = newWeapon;
    }
}
