using UnityEngine;

public class NoWeapon : WeaponObject
{
    // Constants
    private const string HITBOX_PREFAB = "Prefabs/Weapons/Hitbox";

    // Functions
    protected override void OnAttack()
    {
        AttackObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: HITBOX_PREFAB,
            damage: MathUtil.CalculateDamage(bearer.Damage, data.baseDamage),
            knockbackPower: data.knockbackPower,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            knockbackOrigin: transform.position + (transform.forward * 0.5f),
            rotation: transform.rotation,
            parent: transform,
            objectName: "No Weapon Hitbox"
        );

        ObjectFactory.DestroyObject(attackHitbox, 1f);
    }

    protected override void OnAlternateAttack()
    {
        AttackObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: HITBOX_PREFAB,
            damage: MathUtil.CalculateDamage(bearer.Damage, data.baseDamage),
            knockbackPower: data.knockbackPower,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            knockbackOrigin: transform.position + (transform.forward * 0.5f),
            rotation: transform.rotation,
            parent: transform,
            objectName: "No Weapon Hitbox"
        );

        IRigid bearerBody = bearer.Orientation.gameObject.GetComponent<IRigid>();
        bearerBody?.Rigidbody.AddForce((2 * data.knockbackPower * bearer.Orientation.forward) + bearer.Orientation.up, ForceMode.Impulse);

        ObjectFactory.DestroyObject(attackHitbox, 1f);
    }

    protected override void OnSkill()
    {
    }
}