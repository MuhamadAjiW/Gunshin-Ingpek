using System;
using System.Collections;
using UnityEngine;

public class HealingPet : Companion
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "HealingPet";

    // Attributes
    protected IDamageable ownerDamageableComponent;
    [SerializeField] protected float healInterval;
    [SerializeField] protected float healAmount;
    public HealingPetAIController aiController;

    // Events
    public event Action OnHealOwnerEvent;

    // Constructor
    protected new void Start()
    {
        base.Start();

        SetIdPrefix(OBJECT_ID_PREFIX);
        SetLayer(EnvironmentConfig.LAYER_PLAYER); // Healing pet is from player's side

        type = Type.HEALING;
        OnHealOwnerEvent += Heal;

        aiController.Init(this);
    }

    // Function
    protected void Heal()
    {
        Debug.Log($"{id}: Healing {Owner.CompanionController.name}");
        ownerDamageableComponent?.InflictHeal(healAmount);
    }

    private IEnumerator HealDelay()
    {
        yield return new WaitForSeconds(healInterval);
        OnHealOwnerEvent?.Invoke();
        StartCoroutine(HealDelay());
    }

    public override void Assign(IAccompaniable owner)
    {
        base.Assign(owner);

        // Only the player can own a HealingPet
        ownerDamageableComponent = Owner.CompanionController.gameObject.GetComponent<Player>();

#if STRICT
        if (ownerDamageableComponent == null)
        {
            Debug.LogError($"{id}: HealingPet is assigned to a non-Player object");
        }
#endif
    }

    protected new void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(HealDelay());
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
    }

    // Controller functions
    protected override void FixedUpdateAction()
    {
        aiController?.Action();
        Vector3 dampVelocity = new();
        Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, Vector3.zero, ref dampVelocity, GameConfig.MOVEMENT_SMOOTHING);
    }
}