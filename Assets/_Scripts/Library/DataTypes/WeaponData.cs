using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/Weapon/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public float baseDamage;
    public float knockbackPower;
    public float attackInterval;
    public float alternateAttackInterval;
    public GameObject model;
}