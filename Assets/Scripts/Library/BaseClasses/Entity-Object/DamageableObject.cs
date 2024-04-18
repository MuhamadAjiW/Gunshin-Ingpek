using System;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable{
    // Attributes
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    public float MaxHealth {
        get => maxHealth;
        set => maxHealth = value > 0? value : 0;
    }
    public float Health {
        get => health;
        set => health = value > 0? (value > MaxHealth? MaxHealth : value) : 0;
    }

    public bool Damageable => !Dead;
    public bool Dead => health <= 0;
    public event Action OnDeath;
    public event Action OnDamaged;
    public event Action OnHeal;

    // Functions
    public float InflictDamage(float damage){
        Health -= damage;
        OnDamaged?.Invoke();
        if(Dead) OnDeath?.Invoke();

        return Health;
    }

    public float InflictHeal(float heal){
        Health += heal;
        OnHeal?.Invoke();

        return Health;
    }
}