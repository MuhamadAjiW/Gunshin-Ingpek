using System;

public interface IDamageable
{
    // Set-Getters
    float MaxHealth {get; set;}
    float Health {get; set;}
    bool Damageable {get; set; }
    bool Dead {get;}

    // Events
    event Action OnDeathEvent;
    event Action OnDamagedEvent;
    event Action OnHealEvent;

    // Functions
    float InflictDamage(float damage);
    float InflictHeal(float heal);
    float InflictDrainDamage(float damage);
}
