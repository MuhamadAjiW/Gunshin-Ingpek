using UnityEngine;

public class Rifle : WeaponObject
{
    // Constants
    public const string WEAPON_PREFAB = "Prefabs/Weapons/Rifle/Rifle";
    private const string PROJECTILE_PREFAB = "Prefabs/Weapons/Rifle/RifleProjectile";
    private const string HITBOX_PREFAB = "Prefabs/Weapons/Hitbox";
    public const string SHOT_AUDIO_KEY = "Shoot";
    
    // Attribute
    public float fireRange = 200;
    public float projectileSpeed = 100; 
    public AudioController audioController;

    // public MonoBehaviour CompanionController => this;
    
    // Constructor
    protected new void Start()
    {
        base.Start();
        audioController = new AudioController(gameObject, audioController.audios);
    }

    // Function
    protected override void OnAttack()
    {
        audioController.Play(SHOT_AUDIO_KEY);
        ProjectileObject attackProjectile = ObjectFactory.CreateAttackObject<ProjectileObject>(
            prefabPath: PROJECTILE_PREFAB,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, data.baseDamage),
            knockbackPower: data.knockbackPower / 4,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier,
            position: transform.position,
            rotation: bearer.Orientation.rotation,
            knockbackOrigin: transform.position - (bearer.Orientation.forward * projectileSpeed),
            objectName: "Rifle Projectile"
        );

        attackProjectile.travelDistance = fireRange;
        attackProjectile.speed = projectileSpeed;
        attackProjectile.direction = bearer.Orientation.forward;

        IRigid bearerBody = bearer.Orientation.gameObject.GetComponent<IRigid>();
        bearerBody?.Rigidbody.AddForce(-(bearer.Orientation.forward * data.knockbackPower / 16) + bearer.Orientation.up, ForceMode.Impulse);
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
            objectName: "Rifle Hitbox"
        );

        ObjectFactory.DestroyObject(attackHitbox, 0.5f);
    }

    protected override void OnSkill()
    {
        audioController.Play(SHOT_AUDIO_KEY);
        ProjectileObject attackProjectile = ObjectFactory.CreateAttackObject<ProjectileObject>(
            prefabPath: PROJECTILE_PREFAB,
            damage: MathUtils.CalculateDamage(bearer.BaseDamage, data.baseDamage),
            knockbackPower: data.knockbackPower * 2,
            attackLayerCode: bearer.AttackLayerCode,
            damageModifier: bearer.AttackMultiplier * 4,
            position: transform.position + (transform.forward * 0.5f),
            rotation: bearer.Orientation.rotation,
            knockbackOrigin: transform.position - (bearer.Orientation.forward * projectileSpeed),
            objectName: "Rifle Skill Projectile"
        );

        attackProjectile.travelDistance = fireRange * 2;
        attackProjectile.speed = projectileSpeed;
        attackProjectile.direction = bearer.Orientation.forward;

        ObjectFactory.DestroyObject(attackProjectile, 2f);

        IRigid bearerBody = bearer.Orientation.gameObject.GetComponent<IRigid>();
        bearerBody?.Rigidbody.AddForce(-(bearer.Orientation.forward * data.knockbackPower / 16) + bearer.Orientation.up, ForceMode.Impulse);
    }
}