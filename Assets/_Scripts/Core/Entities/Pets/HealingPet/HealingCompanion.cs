using System;
using System.Collections;
using UnityEngine;

public class HealingCompanion : Companion
{
    // Attributes
    protected IDamageable ownerDamageableComponent;
    [SerializeField] protected float healInterval;
    [SerializeField] protected float healAmount;

    // Events
    public event Action OnHealOwnerEvent;

    // Constructor
    protected new void Start()
    {
        base.Start();
    }

    // Function
    protected void Heal()
    {
        Debug.Log($"Healing {Owner.CompanionController.name}");
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
        ownerDamageableComponent = Owner.CompanionController.gameObject.GetComponent<IDamageable>();
        if(ownerDamageableComponent == null)
        {
            Debug.LogWarning("HealingCompanion is assigned to a non-IDamageable object");
        }
    }
}