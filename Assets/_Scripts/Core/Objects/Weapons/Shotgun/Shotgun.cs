using Unity.Mathematics;
using UnityEngine;

public class Shotgun : WeaponObject
{
    // Constants
    public const string weaponPrefab = "Prefabs/Weapons/Shotgun/Shotgun";
    private const string projectilePrefab = "Prefabs/Weapons/Shotgun/ShotgunProjectile";
    private const string hitboxPrefab = "Prefabs/Weapons/Hitbox";
    public const string shotAudioKey = "Shoot";
    
    // Attribute
    public float fireRange = 100;
    public float projectileSpeed = 100; 
    public float spread = 0.2f; 
    public int pelletCount = 10; 
    public AudioController audioController;

    // Constructor
    protected new void Start()
    {
        base.Start();
        audioController = new AudioController(gameObject, audioController.audios);
    }

    // Functions
    protected override void OnAttack()
    {
        audioController.Play(shotAudioKey);
        for (int i = 0; i < pelletCount; i++)
        {
            float damage = MathUtils.CalculateDamage(bearer.BaseDamage, data.baseDamage);
            ShotgunProjectile attackProjectile = ObjectFactory.CreateAttackObject<ShotgunProjectile>(
                prefabPath: projectilePrefab,
                damage: damage,
                knockbackPower: data.knockbackPower,
                attackLayerCode: bearer.AttackLayerCode,
                damageModifier: bearer.AttackMultiplier,
                position: transform.position,
                rotation: bearer.Orientation.rotation,
                knockbackOrigin: transform.position - (bearer.Orientation.forward * projectileSpeed),
                objectName: "Shotgun Projectile"
            );

            float spreadX = UnityEngine.Random.Range(-spread, spread);
            float spreadY = UnityEngine.Random.Range(-spread, spread);

            attackProjectile.travelDistance = fireRange;
            attackProjectile.speed = projectileSpeed;
            attackProjectile.initialDamage = damage;

            Vector3 direction = bearer.Orientation.forward;
            direction += bearer.Orientation.up * spreadY + bearer.Orientation.right * spreadX;

            attackProjectile.direction = direction;
        }
    }

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
            objectName: "Shotgun Hitbox"
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
            objectName: "Shotgun Skill Projectile"
        );

        attackProjectile.travelDistance = fireRange * 2;
        attackProjectile.speed = projectileSpeed;
        attackProjectile.direction = bearer.Orientation.forward;

        ObjectFactory.DestroyObject(attackProjectile, 2f);
    }
}