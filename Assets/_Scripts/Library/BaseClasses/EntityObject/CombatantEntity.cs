using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class CombatantEntity : DamageableEntity, IArmed
{
    // Attributes
    public List<WeaponObject> weaponList = new();
    [SerializeField] protected float baseDamage;
    [SerializeField] protected Vector3 weaponLocation;
    private int weaponIndex;
    private WeaponObject weapon;
    
    // Set-Getters
    public List<WeaponObject> WeaponList => weaponList;
    public WeaponObject Weapon => weapon;
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
        Debug.Log($"Equipping weapon {index}");

        UnequipWeapon();

        Debug.Log($"Equipping weapon {index}");
        WeaponIndex = index;
        WeaponObject selectedWeapon = WeaponList[WeaponIndex];
        
        // To handle editor prefab dragndrops
        Debug.Log($"Length: {WeaponList.Count}");
        if(!selectedWeapon.gameObject.scene.IsValid())
        {
            selectedWeapon = ObjectFactory.CreateObject<WeaponObject>(
                prefabPath: selectedWeapon == null? NoWeapon.weaponPrefab : selectedWeapon.data.prefabPath,
                parent: transform, 
                objectName: EnvironmentConfig.OBJECT_WEAPON
            );
            WeaponList[WeaponIndex] = selectedWeapon;
        }
        selectedWeapon.gameObject.SetActive(true);
        weapon = selectedWeapon;
        // weaponObject.transform.localPosition = WeaponLocation;
        // weaponObject.gameObject.layer = LayerMask.NameToLayer(AttackLayerCode);
        // weapon = weaponObject;
    }

    public void UnequipWeapon(){
        foreach (WeaponObject weapon in WeaponList)
        {
            weapon.gameObject.SetActive(false);
        }
    }
}
