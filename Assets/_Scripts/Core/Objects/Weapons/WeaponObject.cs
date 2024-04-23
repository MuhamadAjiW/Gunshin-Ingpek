using System;
using System.Collections;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour 
{

    // Attributes
    public string prefabPath;
    public WeaponData data;
    protected IArmed bearer;
    private bool canAttack = true;

    // Events
    public event Action OnAttackEvent;
    public event Action OnAlternateAttackEvent;

    // Constructor
    protected void Start()
    {
        bearer = GetComponentInParent<IArmed>();
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
