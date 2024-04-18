using UnityEngine;

public class TestWeapon : WeaponObject{
    // Constants
    private static readonly string hitboxPrefab = "Prefabs/Weapons/TestWeapon/WeaponHitbox";

    // Function
    public override void Attack(){
        Debug.Log("Attacking using a Test Weapon");
        GameObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: hitboxPrefab,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, BaseDamage),
            knockbackPower: KnockbackPower,
            type: bearerType,
            parent: transform
        );

        ObjectFactory.Destroy(attackHitbox, 1f);
    }
}