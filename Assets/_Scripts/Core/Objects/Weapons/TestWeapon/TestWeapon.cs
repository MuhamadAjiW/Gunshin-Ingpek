using UnityEngine;

public class TestWeapon : WeaponObject
{
    // Constants
    public const string weaponPrefab = "Prefabs/Weapons/TestWeapon/TestWeapon";
    private const string hitboxPrefab = "Prefabs/Weapons/TestWeapon/WeaponHitbox";
    private const string projectilePrefab = "Prefabs/Weapons/TestWeapon/TestProjectile";
    
    // Attribute
    public float fireRange = 100;
    public float projectileSpeed = 100; 
    public TestWeaponAnimationController animationController;
    public AudioController audioController;

    // public MonoBehaviour CompanionController => this;
    
    // Constructor
    protected new void Start()
    {
        base.Start();
        animationController = new TestWeaponAnimationController(this);
        audioController = new AudioController(gameObject, audioController.audios);
    }

    // Function
    protected override void OnAttack()
    {
        ProjectileObject attackProjectile = ObjectFactory.CreateAttackObject<ProjectileObject>(
            prefabPath: projectilePrefab,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, data.baseDamage),
            knockbackPower: data.knockbackPower / 4,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            position: transform.position,
            rotation: bearer.Orientation.rotation,
            knockbackOrigin: transform.position - (bearer.Orientation.forward * projectileSpeed),
            objectName: "TestWeapon Projectile"
        );

        attackProjectile.data.travelDistance = fireRange;
        attackProjectile.data.speed = projectileSpeed;
        attackProjectile.direction = bearer.Orientation.forward;

        ObjectFactory.DestroyObject(attackProjectile, 1f);
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
            knockbackOrigin: transform.position + (transform.forward * 0.5f),
            parent: animationController.model.transform,
            objectName: "TestWeapon Hitbox"
        );

        ObjectFactory.DestroyObject(attackHitbox, 0.5f);
    }

    protected override void OnSkill()
    {
        ProjectileObject attackProjectile = ObjectFactory.CreateAttackObject<ProjectileObject>(
            prefabPath: projectilePrefab,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, data.baseDamage),
            knockbackPower: data.knockbackPower * 2,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier * 4,
            position: transform.position + (transform.forward * 0.5f),
            rotation: bearer.Orientation.rotation,
            knockbackOrigin: transform.position - (bearer.Orientation.forward * projectileSpeed),
            objectName: "TestWeapon Skill Projectile"
        );

        attackProjectile.data.travelDistance = fireRange * 2;
        attackProjectile.data.speed = projectileSpeed;
        attackProjectile.direction = bearer.Orientation.forward;

        ObjectFactory.DestroyObject(attackProjectile, 2f);
    }
}