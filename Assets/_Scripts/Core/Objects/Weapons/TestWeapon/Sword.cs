using UnityEngine;

public class Sword : WeaponObject
{
    public const string weaponPrefab = "Prefabs/Weapons/TestWeapon/Sword";
    private const string hitboxPrefab = "Prefabs/Weapons/TestWeapon/WeaponHitbox";
    
    public float attackRange = 2.5f;
    public float attackDamage = 25f; 
    public SwordAnimationController animationController;

    protected new void Start()
    {
        base.Start();
        animationController = new SwordAnimationController(this);
    }

    protected override void OnAttack()
    {
        // Spawn hitbox
        AttackObject hitboxObject = ObjectFactory.CreateAttackObject(
            prefabPath: hitboxPrefab,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, data.baseDamage),
            knockbackPower: data.knockbackPower,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            knockbackOrigin: transform.position,
            parent: transform, // Attach hitbox to the sword
            objectName: "Sword Hitbox"
        );
        ObjectFactory.DestroyObject(hitboxObject, 1f);
    }


    protected override void OnAlternateAttack()
    {
        animationController.AnimateAlternateAttack();

        AttackObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: hitboxPrefab,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, data.baseDamage),
            knockbackPower: data.knockbackPower,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            knockbackOrigin: transform.position,
            parent: animationController.model.transform,
            objectName: "Sword Hitbox"
        );

        ObjectFactory.DestroyObject(attackHitbox, 0.5f);
    }

}
