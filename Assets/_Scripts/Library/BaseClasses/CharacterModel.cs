using UnityEngine;

public class CharacterModel : Model
{
    // Attributes
    public float meleeAnimationDelay;
    public float rangedAnimationDelay;
    public float skillAnimationDelay;
    public Vector3 staticWeaponPivot;
    public Transform dynamicWeaponPivot;

    // Set-Getters
    public Vector3 WeaponPivot => dynamicWeaponPivot == null ? staticWeaponPivot : dynamicWeaponPivot.position;
}