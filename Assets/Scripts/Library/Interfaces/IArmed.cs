using System.Collections.Generic;
using UnityEngine;

public interface IArmed
{
    // Set-Getters
    public float BaseDamage { get; set; }
    public string AttackLayerCode { get; }
    public float AttackMultiplier { get; }
    public Vector3 WeaponLocation { get; }
    public WeaponObject Weapon { get; }
    public Transform Orientation { get; }
}
