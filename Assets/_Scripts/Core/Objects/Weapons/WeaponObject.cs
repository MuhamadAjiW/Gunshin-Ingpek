using System;
using System.Collections;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour 
{

    // Attributes
    public WeaponData data;
    public bool canAttack = true;
    protected IArmed bearer;

    // Events
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

        canAttack = true;
        OnAttackEvent += OnAttack;
        OnAlternateAttackEvent += OnAlternateAttack;
    }

    // Functions
    public virtual bool Attack()
    {
        if(!canAttack)
        {
            return false;
        }
        
        canAttack = false;
        StartCoroutine(DelayAttack(data.attackInterval));
        OnAttackEvent?.Invoke();

        return true;
    }

    public virtual bool AlternateAttack()
    {
        if(!canAttack)
        {
            return false;
        }

        canAttack = false;
        StartCoroutine(DelayAttack(data.alternateAttackInterval));
        OnAlternateAttackEvent?.Invoke();
        
        return true;
    }
    
    private IEnumerator DelayAttack(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    // Abstract Functions
    protected abstract void OnAttack();
    protected abstract void OnAlternateAttack();
}
