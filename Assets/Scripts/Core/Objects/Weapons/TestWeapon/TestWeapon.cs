using UnityEngine;

public class TestWeapon : WeaponObject{
    // Constants
    private static readonly string hitboxPrefab = "Prefabs/Weapons/TestWeapon/WeaponHitbox";
    private static readonly string projectilePrefab = "Prefabs/Weapons/TestWeapon/TestProjectile";
    
    // Attribute
    [SerializeField] private float fireRange = 100;
    [SerializeField] private float projectileSpeed = 100; 

    // Function
    public override void Attack(){
        AttackObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: hitboxPrefab,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, BaseDamage),
            knockbackPower: KnockbackPower,
            type: bearerType,
            knockbackOrigin: transform.position,
            parent: transform,
            objectName: "TestWeapon Hitbox"
        );

        ObjectFactory.DestroyObject(attackHitbox, 1f);
    }

    public override void AttackAlternate(){
        ProjectileObject attackProjectile = ObjectFactory.CreateAttackObject<ProjectileObject>(
            prefabPath: projectilePrefab,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, BaseDamage),
            knockbackPower: KnockbackPower / 4,
            type: bearerType,
            position: transform.position,
            rotation: bearer.Orientation.rotation,
            knockbackOrigin: transform.position - (bearer.Orientation.forward * projectileSpeed),
            objectName: "TestWeapon Projectile"
        );
        attackProjectile.travelDistance = fireRange;
        attackProjectile.speed = projectileSpeed;
        attackProjectile.direction = bearer.Orientation.forward;

        ObjectFactory.DestroyObject(attackProjectile, 1f);
    }
}