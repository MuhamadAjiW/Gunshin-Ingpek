using System;

public interface IDamageable{
    float MaxHealth {get; set;}
    float Health {get; set;}
    bool Damageable {get; set; }
    bool Dead {get;}

    event Action OnDeathEvent;
    event Action OnDamagedEvent;
    event Action OnHealEvent;

    float InflictDamage(float damage);
    float InflictHeal(float heal);
}
