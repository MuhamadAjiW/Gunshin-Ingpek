public class NoWeapon : WeaponObject
{
    // Constants
    private const string HITBOX_PREFAB = "Prefabs/Weapons/Hitbox";
    
    // Functions
    protected override void OnAttack()
    {
        AttackObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: HITBOX_PREFAB,
            damage: MathUtils.CalculateDamage(bearer.Damage, data.baseDamage),
            knockbackPower: data.knockbackPower,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            knockbackOrigin: transform.position + (transform.forward * 0.5f),
            scale: transform.localScale,
            rotation: transform.rotation,
            parent: transform,
            objectName: "No Weapon Hitbox"
        );

        ObjectFactory.DestroyObject(attackHitbox, 1f);
    }

    protected override void OnAlternateAttack()
    {
    }

    protected override void OnSkill()
    {
    }
}