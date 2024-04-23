using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/Weapon/Weapon Data")]
public class WeaponData : ScriptableObject
{
    // Note: prefab system is not quite extendable
    // Learn other systems than this obviously
    // but I think the window to learn and implement a new system is not viable within the scope of the project
    public string prefabPath;
    public GameObject model;
    public float baseDamage;
    public float knockbackPower;
    public float attackInterval;
    public float alternateAttackInterval;
}