using System;
using UnityEngine;

public class DamageableEntity : RigidEntity, IDamageable{
    // Attributes
    private bool damageable = true;
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
    public bool Damageable { 
        get => damageable; 
        set => damageable = value; 
    }
    public bool Dead => health <= 0;
    public event Action OnDeathEvent;
    public event Action OnDamagedEvent;
    public event Action OnHealEvent;

    // Functions
    public float InflictDamage(float damage){
        Health -= damage;
        OnDamagedEvent?.Invoke();
        if(Dead) OnDeathEvent?.Invoke();

        return Health;
    }

    public float InflictHeal(float heal){
        Health += heal;
        OnHealEvent?.Invoke();

        return Health;
    }

    protected new void Start(){
        base.Start();
    }

    protected new void Update(){
        base.Update();
    }

    protected new void FixedUpdate(){
        base.FixedUpdate();
    }
}
