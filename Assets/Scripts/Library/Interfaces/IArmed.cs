using System.Collections.Generic;
using UnityEngine;

public interface IArmed
{
    public float BaseDamage { get; set; }
    public WeaponObject Weapon { get; }
    public Transform Orientation { get; }
}
