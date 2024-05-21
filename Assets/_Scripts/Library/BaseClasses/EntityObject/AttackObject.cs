using System;
using UnityEngine;

// TODO: Review whether attack object should be classified as a world object
public class AttackObject : MonoBehaviour, IDamaging, IKnockback
{
    // Attributes
    private Vector3 knockbackOffset;
    [SerializeField] private float damage;
    [SerializeField] private float knockbackPower;

    // Events
    public event Action OnDamageEvent;

    // Set-Getters
    public float Damage
    {
        get => damage;
        set => damage = value;
    }
    public float KnockbackPower
    {
        get => knockbackPower;
        set => knockbackPower = value;
    }
    public Vector3 KnockbackOrigin
    {
        get => transform.position + knockbackOffset;
        set => knockbackOffset = KnockbackOrigin - transform.position;
    }


    // Constructor
    protected void Start()
    {
        if (KnockbackOrigin == null)
        {
            KnockbackOrigin = Vector3.zero;
        }
    }

    // Functions
    public void Knockback(IRigid rigidObject)
    {
        var knockbackModifier = (-1) * knockbackPower / rigidObject.KnockbackResistance;
        Vector3 knockbackVector = MathUtil.GetDirectionVectorFlat(KnockbackOrigin, rigidObject.Position) * knockbackModifier;
        rigidObject.Rigidbody.AddForce(knockbackVector, ForceMode.Impulse);
    }

    protected bool Hit(Collider otherCollider)
    {
        // Note: Hitboxes are traditionally placed within a model, therefore we get the damageable component from its parent
        Transform objectParent = otherCollider.transform.parent;
        if (objectParent == null)
        {
            return true;
        }
        objectParent.TryGetComponent<IDamageable>(out var damageableObject);
        if (damageableObject == null)
        {
            return true;
        }


        if (damageableObject.Damageable)
        {
            Debug.Log($"Hit in hitbox of {transform.name} by {objectParent.name} with damage of {Damage}");
            Debug.Log($"Attack object from layer: {gameObject.layer}, {LayerMask.LayerToName(gameObject.layer)}");

            bool isVictimPlayer = objectParent.gameObject.TryGetComponent<Player>(out var _);
            bool isVictimPlayerPet = objectParent.gameObject.TryGetComponent<AttackingPet>(out var _) || objectParent.gameObject.TryGetComponent<HealingPet>(out var _);
            bool isPlayerAttacker = LayerMask.LayerToName(gameObject.layer) == EnvironmentConfig.LAYER_PLAYER_ATTACK;

            // CHEAT CHECKING
            // Player
            if (isVictimPlayer && GameController.Instance.cheatController.NO_DAMAGE)
            {
                Debug.Log("NO_DAMAGE cheat is active! Skipping damage.");
                return false;
            }

            // Player's Pet
            if (isVictimPlayerPet && GameController.Instance.cheatController.FULL_HP_PET)
            {
                Debug.Log("FULL_HP_PET cheat is active! Skipping damage.");
                return false;
            }

            // Player attacking with one hit kill
            if (isPlayerAttacker && GameController.Instance.cheatController.ONE_HIT_KILL)
            {
                Debug.Log("ONE_HIT_KILL cheat is active! Inflicting damage.");
                Damage = damageableObject.Health;
            }

            damageableObject.InflictDamage(Damage);

            OnDamageEvent?.Invoke();

            objectParent.TryGetComponent<IRigid>(out var rigidObject);
            if (rigidObject != null)
            {
                Knockback(rigidObject);
            }

            return true;
        }
        return false;
    }
}
