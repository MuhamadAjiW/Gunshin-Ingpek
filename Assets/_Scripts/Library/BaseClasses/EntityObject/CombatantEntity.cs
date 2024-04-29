using System;
using System.Collections.Generic;
using System.Reflection;
using _Scripts.Core.Game.Data;
using Unity.VisualScripting;
using UnityEngine;

public class CombatantEntity : DamageableEntity, IArmed
{
    // Attributes
    [SerializeField] protected float baseDamage;
    public List<WeaponObject> weaponList = new();
    private int weaponIndex;
    private WeaponObject weapon;
    private float attackMultiplier = 1f;
    private string attackLayerCode = EnvironmentConfig.LAYER_ENVIRONMENT_ATTACK;
    
    // Set-Getters
    public List<WeaponObject> WeaponList => weaponList;
    public WeaponObject Weapon => weapon;
    public Transform Orientation => transform;
    public Vector3 WeaponLocation => model.WeaponPivot;
    public float AttackMultiplier
    {
        get => attackMultiplier;
        set => attackMultiplier = value;
    }
    public string AttackLayerCode
    {
        get => attackLayerCode;
        set => attackLayerCode = value;
    }
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

    // Constructors
    protected new void Start()
    {
        base.Start();
        #if STRICT
        if(WeaponList.Count ==  0)
        {
            Debug.LogError($"CombatantEntity {name} does not have any initial weapon. How to solve: Consider putting a NoWeapon instead to the list in the class");
        }
        #endif
    }


    // Functions
    public void SetAttackLayer(string attackLayerCode)
    {
        AttackLayerCode = attackLayerCode;
        AttackMultiplier = attackLayerCode switch 
        {
            EnvironmentConfig.LAYER_ENEMY_ATTACK => GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.Instance.difficulty].enemyDamageMultiplier,
            EnvironmentConfig.LAYER_PLAYER_ATTACK => GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.Instance.difficulty].playerDamageMultiplier,
            _ => 1f
        };
    }
    
    public void EquipWeapon(int index)
    {
        if(weaponList.Count == 0)
        {
            return;
        }
        if(Weapon != null && !Weapon.CanAttack)
        {
            return;
        }
        
        UnequipWeapon();

        WeaponIndex = index;
        WeaponObject selectedWeapon = WeaponList[WeaponIndex];
        Debug.Log($"Equipping weapon {selectedWeapon.name}");
        
        // To handle prefabs
        if(!selectedWeapon.gameObject.scene.IsValid())
        {
            Debug.Log($"Bearer: {gameObject.name}");
            Debug.Log($"DynamicPivot: {model.dynamicWeaponPivot != null}");
            Debug.Log($"Location: {WeaponLocation}");
            if(model.dynamicWeaponPivot != null)
            {
                Debug.Log("Dynamic");
                selectedWeapon = ObjectFactory.CreateObject<WeaponObject>(
                    prefabPath: selectedWeapon.data.prefabPath,
                    parent: model.dynamicWeaponPivot, 
                    objectName: selectedWeapon.name
                );
                WeaponList[WeaponIndex] = selectedWeapon;
            }
            else
            {
                Debug.Log("Static");
                selectedWeapon = ObjectFactory.CreateObject<WeaponObject>(
                    prefabPath: selectedWeapon.data.prefabPath,
                    parent: gameObject.transform, 
                    objectName: selectedWeapon.name
                );
                WeaponList[WeaponIndex] = selectedWeapon;
                // selectedWeapon.transform.localPosition = WeaponLocation;
            }
            
            Debug.Log($"Created weapon local location: {selectedWeapon.transform.localPosition}");
            Debug.Log($"Created weapon absolute location: {selectedWeapon.transform.position}");
            Debug.Log($"Created weapon parent: {selectedWeapon.transform.parent.name}");
        }

        selectedWeapon.gameObject.SetActive(true);
        selectedWeapon.gameObject.layer = LayerMask.NameToLayer(AttackLayerCode);
        weapon = selectedWeapon;        
        Weapon.transform.localScale = model.weaponScale != Vector3.zero ? model.weaponScale : Vector3.one;
    }

    public void UnequipWeapon()
    {
        List<WeaponObject> nullObjects = new();
        foreach (WeaponObject weapon in WeaponList)
        {
            // We'll also clean null weapons here, add no weapons explicitly
            if(weapon == null)
            {
                nullObjects.Add(weapon);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }

        foreach (WeaponObject nullWeapons in nullObjects)
        {
            weaponList.Remove(nullWeapons);
        }
    }
}
