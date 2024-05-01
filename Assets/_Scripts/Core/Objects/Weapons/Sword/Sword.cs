
using UnityEngine;

public class Sword : WeaponObject
{
    // Constants
    private const string weaponPrefab = "Prefabs/Weapons/Sword/Sword";
    private const string hitboxPrefab = "Prefabs/Weapons/Hitbox";

    // Functions
    protected override void OnAlternateAttack()
    {
        AttackObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: hitboxPrefab,
            damage: MathUtils.CalculateDamage(bearer.Damage, data.baseDamage),
            knockbackPower: data.knockbackPower,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            knockbackOrigin: transform.position + (transform.forward * 0.5f),
            parent: transform,
            objectName: "Sword Hitbox"
        );

        ObjectFactory.DestroyObject(attackHitbox, 0.5f);
    }

    protected override void OnAttack()
    {
        AttackObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: hitboxPrefab,
            damage: MathUtils.CalculateDamage(bearer.Damage, data.baseDamage),
            knockbackPower: data.knockbackPower,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            knockbackOrigin: transform.position + (transform.forward * 0.5f),
            parent: transform,
            objectName: "Sword Hitbox"
        );

        ObjectFactory.DestroyObject(attackHitbox, 0.5f);
    }

    protected override void OnSkill()
    {
        Debug.Log("Not implemented yet ehe");
    }
}