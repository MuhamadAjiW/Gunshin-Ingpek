using Unity.Mathematics;
using UnityEngine;

public class Shotgun : WeaponObject
{
    // Constants
    public const string WEAPON_PREFAB = "Prefabs/Weapons/Shotgun/Shotgun";
    private const string PROJECTILE_PREFAB = "Prefabs/Weapons/Shotgun/ShotgunProjectile";
    private const string HITBOX_PREFAB = "Prefabs/Weapons/Hitbox";
    public const string SHOT_AUDIO_KEY = "Shoot";
    
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
        audioController.Play(SHOT_AUDIO_KEY);
        float damage = MathUtils.CalculateDamage(bearer.BaseDamage, data.baseDamage);
        ShotgunProjectile attackProjectile = ObjectFactory.CreateAttackObject<ShotgunProjectile>(
            prefabPath: PROJECTILE_PREFAB,
            damage: damage,
            knockbackPower: data.knockbackPower,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            position: transform.position,
            rotation: bearer.Orientation.rotation,
            knockbackOrigin: transform.position - (bearer.Orientation.forward * projectileSpeed),
            objectName: "Shotgun Projectile"
        );

        attackProjectile.travelDistance = fireRange;
        attackProjectile.speed = projectileSpeed;
        attackProjectile.initialDamage = damage;

        Vector3 direction = bearer.Orientation.forward;
        attackProjectile.direction = direction;

        for (int i = 0; i < pelletCount; i++)
        {
            attackProjectile = ObjectFactory.CreateAttackObject<ShotgunProjectile>(
                prefabPath: PROJECTILE_PREFAB,
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

            direction = bearer.Orientation.forward;
            direction += bearer.Orientation.up * spreadY + bearer.Orientation.right * spreadX;

            attackProjectile.direction = direction;
        }

        IRigid bearerBody = bearer.Orientation.gameObject.GetComponent<IRigid>();
        bearerBody?.Rigidbody.AddForce(-(bearer.Orientation.forward * data.knockbackPower) + bearer.Orientation.up, ForceMode.Impulse);
    }

    protected override void OnAlternateAttack()
    {
        AttackObject attackHitbox = ObjectFactory.CreateAttackObject(
            prefabPath: HITBOX_PREFAB,
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
        audioController.Play(SHOT_AUDIO_KEY);
        float damage = MathUtils.CalculateDamage(bearer.BaseDamage, data.baseDamage);
        ShotgunProjectile attackProjectile = ObjectFactory.CreateAttackObject<ShotgunProjectile>(
            prefabPath: PROJECTILE_PREFAB,
            damage: damage,
            knockbackPower: data.knockbackPower,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            position: transform.position,
            rotation: bearer.Orientation.rotation,
            knockbackOrigin: transform.position - (bearer.Orientation.forward * projectileSpeed),
            objectName: "Shotgun Projectile"
        );

        attackProjectile.travelDistance = fireRange;
        attackProjectile.speed = projectileSpeed;
        attackProjectile.initialDamage = damage;

        Vector3 direction = bearer.Orientation.forward;
        attackProjectile.direction = direction;

        for (int i = 0; i < pelletCount; i++)
        {
            attackProjectile = ObjectFactory.CreateAttackObject<ShotgunProjectile>(
                prefabPath: PROJECTILE_PREFAB,
                damage: damage,
                knockbackPower: data.knockbackPower * 3,
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

            direction = bearer.Orientation.forward;
            direction += bearer.Orientation.up * spreadY + bearer.Orientation.right * spreadX;

            attackProjectile.direction = direction;
        }

        IRigid bearerBody = bearer.Orientation.gameObject.GetComponent<IRigid>();
        bearerBody?.Rigidbody.AddForce(-(bearer.Orientation.forward * data.knockbackPower) + bearer.Orientation.up, ForceMode.Impulse);
    }
}