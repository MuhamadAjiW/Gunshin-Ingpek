using UnityEngine;

public interface IArmed{
    public float BaseDamage { get; set; }
    public WeaponObject Weapon { get; set; }
    public Transform Orientation { get; }
}
