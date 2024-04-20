using System;
using System.Collections;
using UnityEngine;

public abstract class DamageableEntityStateController : EntityStateController {
    // Attributes
    private readonly DamageableEntity Entity;
    private float damagedDelay;
    public event Action OnDamageDelayOverEvent;
    
    // Set-Getters
    public float DamagedDelay {
        get => damagedDelay;
        set => damagedDelay = value <= 0? GameConfig.DAMAGED_DELAY_DURATION : value;
    }

    // Constructor
    public DamageableEntityStateController(DamageableEntity entity, float delay = 0){
        Entity = entity;
        DamagedDelay =  delay;

        OnDamageDelayOverEvent += OnDamageDelayOver;
        Entity.OnDamagedEvent += OnDamaged;
        Entity.Damageable = true;
    }

    // Functions
    private IEnumerator WaitDamagedDelay(){
        if (!Entity.Dead){
            yield return new WaitForSeconds(DamagedDelay);
            Entity.Damageable = true;
            InvokeDamageDelayOver();
        }
    }

    private void InvokeDamageDelayOver(){
        OnDamageDelayOverEvent?.Invoke();
    }

    private void OnDamaged(){
        Entity.Damageable = false;
        Entity.StartCoroutine(WaitDamagedDelay());
    }
    
    private void OnDamageDelayOver(){
        Entity.Damageable = true;
    }
}
