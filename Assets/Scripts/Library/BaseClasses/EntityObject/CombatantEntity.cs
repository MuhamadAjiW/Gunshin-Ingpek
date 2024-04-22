using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatantEntity : DamageableEntity, IArmed
{
    // Attributes
    [SerializeField] private float baseDamage;
    public List<WeaponObject> weaponList = new();
    private int weaponIndex;
    
    // Set-Getters
    public List<WeaponObject> WeaponList => weaponList;
    public WeaponObject Weapon => weaponList.Count > 0? weaponList[WeaponIndex] : null;
    public Transform Orientation => transform;
    public float BaseDamage 
    {
        get => baseDamage;
        set => baseDamage = value;
    }
    public int WeaponIndex
    {
        get => weaponIndex;
        set 
        {
            // Switch requires a constant, so can't use that here
            if(value == weaponList.Count) weaponIndex = 0;
            else if(value == -1) weaponIndex = weaponList.Count - 1;
            else if(-1 < value && value < weaponList.Count) weaponIndex = value;
            else weaponIndex = 0;
        } 
    }

    // Functions
    public void SetWeapon(int index)
    {
        if(weaponList.Count == 0)
        {
            return;
        }

        WeaponIndex = index;
    }
}
