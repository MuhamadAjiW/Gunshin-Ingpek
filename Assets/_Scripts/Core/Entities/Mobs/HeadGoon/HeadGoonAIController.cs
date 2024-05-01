using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


// TODO: Refactor a bit as EnemyAIController
// Lotsa codes are reusable
[Serializable]
public class HeadGoonAIController
{
    // Attributes
    public readonly NavMeshAgent nav;
    protected float attackWindowSize = 0.3f;
    protected Coroutine attackWindowCoroutine;
    private readonly HeadGoon headGoon;

    // Constructor
    public HeadGoonAIController(HeadGoon headGoon)
    {
        this.headGoon = headGoon;
        nav = headGoon.GetComponent<NavMeshAgent>();
        headGoon.OnDamagedEvent += OnDamaged;
        headGoon.OnDeathEvent += OnDeath;
    }

    // Functions
    public void Action()
    {
        switch (HeadGoonState.GetAIState(headGoon.stateController.State))
        {
            case HeadGoonState.AI_DETECTED_STATE:
                GoToward(GameController.Instance.player.transform);
                break;
            case HeadGoonState.AI_IN_RANGE_STATE:
                GoToward(headGoon.transform);
                Quaternion targetAngle = LookToward(GameController.Instance.player.transform);
                if(Quaternion.Angle(targetAngle, headGoon.transform.rotation) < 10)
                {
                    Attack();
                }
                break;
        }
    }

    public Quaternion LookToward(Transform target)
    {
        Vector3 direction = MathUtils.GetDirectionVectorFlat(target.position, headGoon.Position);
        Quaternion look = Quaternion.LookRotation(direction);
        headGoon.transform.rotation = Quaternion.Slerp(look, headGoon.transform.rotation, Time.deltaTime);
        return look;
    }

    public void GoToward(Transform target)
    {
        nav.destination = target.position;
    }

    public void Attack()
    {
        if(headGoon.Weapon == null)
        {
            return;
        }
        headGoon.StartCoroutine(HandleAttack());
    }

    public void AlternateAttack()
    {
        if(headGoon.Weapon == null)
        {
            return;
        }
        headGoon.StartCoroutine(HandleAlternateAttack());
    }

    public IEnumerator HandleAttack()
    {
        float delay = headGoon.Weapon.attackType switch
        {
            AttackType.MELEE => headGoon.model.meleeAnimationDelay,
            AttackType.RANGED => headGoon.model.rangedAnimationDelay,
            _ => 0
        };
    
        headGoon.animationController.AnimateAttack(headGoon.Weapon.attackType);
        
        // TODO: Implement AudioController

        // if(headGoon.Weapon.CanAttack)
        // {
        //     if(headGoon.Weapon.attackType == AttackType.MELEE 
        //         || headGoon.stateController.weaponState != WeaponState.ATTACK)
        //     {
        //         headGoon.audioController.Play(PlayerAudioController.ATTACK_KEY);
        //     }
        // }

        TriggerWeaponState(WeaponState.ATTACK);
        yield return new WaitForSeconds(delay);
        headGoon.Weapon.Attack();
    }

    public IEnumerator HandleAlternateAttack()
    {
        float delay = headGoon.Weapon.alternateAttackType switch
        {
            AttackType.MELEE => headGoon.model.meleeAnimationDelay,
            AttackType.RANGED => headGoon.model.rangedAnimationDelay,
            _ => 0
        };
    
        headGoon.animationController.AnimateAttack(headGoon.Weapon.attackType);
        
        // TODO: Implement AudioController

        // if(headGoon.Weapon.CanAttack)
        // {
        //     if(headGoon.Weapon.attackType == AttackType.MELEE 
        //         || headGoon.stateController.weaponState != WeaponState.ATTACK)
        //     {
        //         headGoon.audioController.Play(PlayerAudioController.ATTACK_KEY);
        //     }
        // }

        TriggerWeaponState(WeaponState.ALTERNATE_ATTACK);
        yield return new WaitForSeconds(delay);
        headGoon.Weapon.AlternateAttack();
    }

    private IEnumerator HandleAttackWindow(float attackWindow)
    {
        yield return new WaitForSeconds(attackWindow);
        headGoon.stateController.ClearWeaponState();
    }

    private void TriggerWeaponState(WeaponState state)
    {
        headGoon.stateController.SetWeaponState(state);
        if(attackWindowCoroutine != null)
        {
            headGoon.StopCoroutine(attackWindowCoroutine);
        }

        float delay = state switch
        {
            WeaponState.ATTACK => headGoon.Weapon.data.attackInterval,
            WeaponState.ALTERNATE_ATTACK => headGoon.Weapon.data.alternateAttackInterval,
            WeaponState.SKILL => headGoon.Weapon.data.skillInterval,
            _ => 0
        };

        attackWindowCoroutine = headGoon.StartCoroutine(HandleAttackWindow(delay + attackWindowSize));
    }

    private void OnDamaged()
    {
        nav.velocity /= 2;
    }

    private void OnDeath()
    {
        GoToward(headGoon.transform);
        nav.velocity = Vector3.zero;
    }
}