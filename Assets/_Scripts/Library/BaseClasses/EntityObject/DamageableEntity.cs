using System;
using System.Collections;
using UnityEngine;

public class DamageableEntity : WorldEntity, IDamageable
{
    // Attributes
    private bool damageable = true;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float damagedDelay;

    // Events
    public event Action OnDeathEvent;
    public event Action OnDamagedEvent;
    public event Action OnHealEvent;
    public event Action OnDamageDelayOverEvent;

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
    public float DamagedDelay 
    {
        get => damagedDelay;
        set => damagedDelay = value <= 0? GameConfig.DAMAGED_DELAY_DURATION : value;
    }

    // Constructor
    protected new void Start(){
        base.Start();
        OnDamageDelayOverEvent += OnDamageDelayOver;
        OnDamagedEvent += OnDamaged;
        Damageable = true;
    }

    // Functions
    protected new void Update()
    {
        if(GameController.Instance.IsPaused || Dead)
        {
            return;
        }
        UpdateAction();
    }

    protected new void FixedUpdate()
    {
        if(GameController.Instance.IsPaused || Dead)
        {
            return;
        }
        base.FixedUpdate();
        FixedUpdateAction();
    }

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

    private IEnumerator WaitDamagedDelay()
    {
        if (!Dead)
        {
            Debug.Log("Damage delay over");
            yield return new WaitForSeconds(DamagedDelay);
            Damageable = true;
            InvokeDamageDelayOver();
        }
    }

    private void InvokeDamageDelayOver()
    {
        OnDamageDelayOverEvent?.Invoke();
    }

    private void OnDamaged()
    {
        Damageable = false;
        StartCoroutine(WaitDamagedDelay());
    }
    
    private void OnDamageDelayOver()
    {
        Damageable = true;
    }
}
