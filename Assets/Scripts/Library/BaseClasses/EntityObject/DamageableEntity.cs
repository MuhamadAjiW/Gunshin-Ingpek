using System;
using UnityEngine;

public class DamageableEntity : WorldEntity, IDamageable
{
    // Attributes
    private bool damageable = true;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    // Events
    public event Action OnDeathEvent;
    public event Action OnDamagedEvent;
    public event Action OnHealEvent;

    // Set-Getters
    public bool Dead => health <= 0;
    public float MaxHealth 
    {
        get => maxHealth;
        set => maxHealth = value > 0? value : 0;
    }
    public float Health 
    {
        get => health;
        set => health = value > 0? (value > MaxHealth? MaxHealth : value) : 0;
    }
    public bool Damageable 
    { 
        get => damageable; 
        set => damageable = value; 
    }

    // Functions
    public float InflictDamage(float damage)
    {
        Health -= damage;
        OnDamagedEvent?.Invoke();
        if(Dead)
        {
            OnDeathEvent?.Invoke();
        }

        return Health;
    }

    public float InflictHeal(float heal)
    {
        Health += heal;
        OnHealEvent?.Invoke();

        return Health;
    }
}
