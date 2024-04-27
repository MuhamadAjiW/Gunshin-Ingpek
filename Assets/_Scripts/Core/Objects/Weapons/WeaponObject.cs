using System;
using System.Collections;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour 
{

    // Attributes
    public WeaponData data;
    public WeaponState state;
    public AttackType attackType;
    public AttackType alternateAttackType;
    protected IArmed bearer;

    // Set-Getters
    public bool CanAttack => state == WeaponState.IDLE;
    public AttackType CurrentAttackType => state switch
    {
        WeaponState.ATTACK => attackType,
        WeaponState.ALTERNATE_ATTACK => alternateAttackType,
        _ => AttackType.NULL
    };

    // Events
    public event Action OnAttackFinished;
    public event Action OnAttackEvent;
    public event Action OnAlternateAttackEvent;

    // Constructor
    protected void Start()
    {
        bearer = GetComponentInParent<IArmed>();
        
        #if STRICT
        if(bearer == null)
        {
            Debug.LogError("Weapon object is assigned to a non-IArmed parent. How to resolve: create one or assign it to something else");
        }
        #endif

        state = WeaponState.IDLE;
        OnAttackEvent += OnAttack;
        OnAlternateAttackEvent += OnAlternateAttack;
    }

    // Functions
    public virtual bool Attack()
    {
        if(!CanAttack)
        {
            return false;
        }
        
        state = WeaponState.ATTACK;
        StartCoroutine(DelayAttack(data.attackInterval));
        OnAttackEvent?.Invoke();

        return true;
    }

    public virtual bool AlternateAttack()
    {
        if(!CanAttack)
        {
            return false;
        }

        state = WeaponState.ALTERNATE_ATTACK;
        StartCoroutine(DelayAttack(data.alternateAttackInterval));
        OnAlternateAttackEvent?.Invoke();
        
        return true;
    }

    protected void FixedUpdate()
    {
        bearer = GetComponentInParent<IArmed>();

        if(transform.position != bearer.WeaponLocation)
        {
            transform.position = bearer.WeaponLocation;
        }
    }
    
    private IEnumerator DelayAttack(float time)
    {
        yield return new WaitForSeconds(time);
        state = WeaponState.IDLE;
        OnAttackFinished?.Invoke();
    }

    // Abstract Functions
    protected abstract void OnAttack();
    protected abstract void OnAlternateAttack();
}
