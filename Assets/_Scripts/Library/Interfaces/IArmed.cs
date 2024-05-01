using System.Collections.Generic;
using UnityEngine;

public interface IArmed
{
    // Set-Getters
    public float Damage { get; set; }
    public string AttackLayerCode { get; set; }
    public float AttackMultiplier { get; set; }
    public Vector3 WeaponLocation { get; }
    public WeaponObject Weapon { get; }
    public Transform Orientation { get; }
}
