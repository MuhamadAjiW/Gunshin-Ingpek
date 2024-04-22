using System.Collections.Generic;
using UnityEngine;

public interface IArmed
{
    // Set-Getters
    public float BaseDamage { get; set; }
    public WeaponObject Weapon { get; }
    public Transform Orientation { get; }
}
