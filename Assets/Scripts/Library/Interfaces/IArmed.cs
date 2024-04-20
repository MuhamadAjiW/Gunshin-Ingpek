using UnityEngine;

public interface IArmed{
    public float BaseDamage { get; set; }
    public WeaponObject Weapon { get; set; }
    public Vector3 Front { get; }
    public Quaternion Rotation { get; }
}
