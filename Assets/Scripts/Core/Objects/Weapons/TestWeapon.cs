using UnityEngine;

public class TestWeapon : WeaponObject{
    // Constants
    private static readonly string hitboxPrefab = "Prefabs/Weapons/TestWeapon/WeaponHitbox";

    // Function
    public override void Attack(){
        GameObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: hitboxPrefab,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, BaseDamage),
            knockbackPower: KnockbackPower,
            type: bearerType,
            knockbackOrigin: transform.position,
            parent: transform,
            objectName: "TestWeapon Hitbox"
        );

        ObjectFactory.Destroy(attackHitbox, 1f);
    }
}