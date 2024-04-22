using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CombatantEntity : DamageableEntity, IArmed
{
    // Attributes
    [SerializeField] protected float baseDamage;
    [SerializeField] protected Vector3 weaponLocation;
    public List<WeaponObject> weaponList = new();
    private int weaponIndex;
    
    // Set-Getters
    public List<WeaponObject> WeaponList => weaponList;
    public WeaponObject Weapon => weaponList.Count > 0? weaponList[WeaponIndex] : null;
    public Transform Orientation => transform;
    public Vector3 WeaponLocation => weaponLocation;
    public string AttackLayerCode => EnvironmentConfig.LAYER_ENVIRONMENT_ATTACK;
    public float AttackMultiplier => 1f;
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
    // TODO: Review, consider using prefabs instead?
    // Prefabs are easier to implement but much less extendable
    public void EquipWeapon(int index)
    {
        if(weaponList.Count == 0)
        {
            return;
        }
        Debug.Log($"Equipping weapon {WeaponIndex}");

        UnequipWeapon();

        WeaponIndex = index;

        WeaponObject weaponObject = ObjectFactory.CreateObject<WeaponObject>(
            prefabPath: Weapon.prefabPath,
            parent: transform, 
            position: WeaponLocation,
            objectName: EnvironmentConfig.OBJECT_WEAPON
        );
        weaponObject.gameObject.layer = LayerMask.NameToLayer(AttackLayerCode);
        WeaponList[weaponIndex] = weaponObject;

        Debug.Log(Weapon == null);
    }

    public void UnequipWeapon(){
        foreach (WeaponObject weapon in GetComponentsInChildren<WeaponObject>())
        {
            Destroy(weapon.gameObject);
        }
    }
}
