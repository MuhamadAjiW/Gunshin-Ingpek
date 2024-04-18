using System;
using System.Collections;
using UnityEngine;

public abstract class DamageableEntityStateController : EntityStateController {
    // Attributes
    private readonly DamageableEntity Entity;
    private float damagedDelay;
    protected bool damaged = false;
    public bool Damaged => damaged;
    public float DamagedDelay {
        get => damagedDelay;
        set => damagedDelay = value <= 0? GameConfig.DAMAGED_DELAY_DURATION : value;
    }
    public event Action OnDamageDelayOverEvent;

    // Constructor
    public DamageableEntityStateController(DamageableEntity entity, float delay = 0){
        Entity = entity;
        DamagedDelay =  delay;

        OnDamageDelayOverEvent += OnDamageDelayOver;
        entity.OnDamagedEvent += OnDamaged;
    }

    // Functions
    private IEnumerator WaitDamagedDelay(){
        if (!Entity.Dead){
            yield return new WaitForSeconds(DamagedDelay);
            damaged = false;
            InvokeDamageDelayOver();
        }
    }

    private void InvokeDamageDelayOver(){
        OnDamageDelayOverEvent?.Invoke();
    }

    private void OnDamaged(){
        damaged = true;
        InvokeOnStateChanged();
        Entity.StartCoroutine(WaitDamagedDelay());
    }
    private void OnDamageDelayOver(){
        damaged = false;
        InvokeOnStateChanged();
    }
}
