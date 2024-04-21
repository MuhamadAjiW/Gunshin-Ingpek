using System;
using System.Collections;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour {
    // Attributes
    protected AttackObjectType bearerType;
    protected IArmed bearer;
    [SerializeField] protected float BaseDamage;
    [SerializeField] protected float KnockbackPower;
    [SerializeField] protected float AttackInterval;
    [SerializeField] protected float AlternateAttackInterval;
    public event Action OnAttackEvent;
    public event Action OnAlternateAttackEvent;
    [SerializeField] private bool canAttack;

    // Constructor
    protected void Start(){
        bearer = GetComponentInParent<IArmed>();
        if(bearer is Player) bearerType = AttackObjectType.PLAYER;
        else if(bearer is EnemyEntity) bearerType = AttackObjectType.ENEMY;
        else bearerType = AttackObjectType.ENVIRONMENT;

        canAttack = true;
        OnAttackEvent += OnAttack;
        OnAlternateAttackEvent += OnAlternateAttack;
    }

    // Functions
    public virtual bool Attack(){
        if(!canAttack) return false;
        canAttack = false;
        StartCoroutine(DelayAttack(AttackInterval));
        OnAttackEvent?.Invoke();
        return true;
    }

    public virtual bool AlternateAttack(){
        if(!canAttack) return false;
        canAttack = false;
        StartCoroutine(DelayAttack(AlternateAttackInterval));
        OnAlternateAttackEvent?.Invoke();
        return true;
    }
    
    protected abstract void OnAttack();
    protected abstract void OnAlternateAttack();

    private IEnumerator DelayAttack(float time){
        yield return new WaitForSeconds(time);
        canAttack = true;
    }
}
