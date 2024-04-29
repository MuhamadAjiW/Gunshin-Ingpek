
using UnityEngine;

public class DarkIronSword : WeaponObject
{
    // Constants
    private const string weaponPrefab = "Prefabs/Weapons/DarkIronSword/DarkIronSword";
    private const string hitboxPrefab = "Prefabs/Weapons/TestWeapon/WeaponHitbox";

    // Functions
    protected override void OnAlternateAttack()
    {
        AttackObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: hitboxPrefab,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, data.baseDamage),
            knockbackPower: data.knockbackPower,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            knockbackOrigin: transform.position + (transform.forward * 0.5f),
            parent: transform,
            objectName: "DarkIronSword Hitbox"
        );

        ObjectFactory.DestroyObject(attackHitbox, 0.5f);
    }

    protected override void OnAttack()
    {
        AttackObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: hitboxPrefab,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, data.baseDamage),
            knockbackPower: data.knockbackPower,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            knockbackOrigin: transform.position + (transform.forward * 0.5f),
            parent: transform,
            objectName: "DarkIronSword Hitbox"
        );

        ObjectFactory.DestroyObject(attackHitbox, 0.5f);
    }

    protected override void OnSkill()
    {
        Debug.Log("Skill");
    }
}