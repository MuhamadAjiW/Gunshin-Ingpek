using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


// TODO: Refactor a bit as EnemyAIController
// Lotsa codes are reusable
[Serializable]
public class GeneralAIController
{
    // Attributes
    public readonly NavMeshAgent nav;
    protected float attackWindowSize = 0.3f;
    protected Coroutine attackWindowCoroutine;
    private readonly General general;

    // Constructor
    public GeneralAIController(General general)
    {
        this.general = general;
        nav = general.GetComponent<NavMeshAgent>();
        general.OnDamagedEvent += OnDamaged;
        general.OnDeathEvent += OnDeath;
    }

    // Functions
    public void Action()
    {
        switch (GeneralState.GetAIState(general.stateController.State))
        {
            case GeneralState.AI_DETECTED_STATE:
                GoToward(GameController.Instance.player.transform);
                break;
            case GeneralState.AI_IN_RANGE_STATE:
                GoToward(general.transform);
                Quaternion targetAngle = LookToward(GameController.Instance.player.transform);
                if(Quaternion.Angle(targetAngle, general.transform.rotation) < 10)
                {
                    Attack();
                }
                break;
        }
    }

    public Quaternion LookToward(Transform target)
    {
        Vector3 direction = MathUtils.GetDirectionVectorFlat(target.position, general.Position);
        Quaternion look = Quaternion.LookRotation(direction);
        general.transform.rotation = Quaternion.Slerp(look, general.transform.rotation, Time.deltaTime);
        return look;
    }

    public void GoToward(Transform target)
    {
        nav.destination = target.position;
    }

    public void Attack()
    {
        if(general.Weapon == null)
        {
            return;
        }
        general.StartCoroutine(HandleAttack());
    }

    public void AlternateAttack()
    {
        if(general.Weapon == null)
        {
            return;
        }
        general.StartCoroutine(HandleAlternateAttack());
    }

    public IEnumerator HandleAttack()
    {
        float delay = general.Weapon.attackType switch
        {
            AttackType.MELEE => general.model.meleeAnimationDelay,
            AttackType.RANGED => general.model.rangedAnimationDelay,
            _ => 0
        };
    
        general.animationController.AnimateAttack(general.Weapon.attackType);
        
        // TODO: Implement AudioController

        // if(general.Weapon.CanAttack)
        // {
        //     if(general.Weapon.attackType == AttackType.MELEE 
        //         || general.stateController.weaponState != WeaponState.ATTACK)
        //     {
        //         general.audioController.Play(PlayerAudioController.ATTACK_KEY);
        //     }
        // }

        TriggerWeaponState(WeaponState.ATTACK);
        yield return new WaitForSeconds(delay);
        general.Weapon.Attack();
    }

    public IEnumerator HandleAlternateAttack()
    {
        float delay = general.Weapon.alternateAttackType switch
        {
            AttackType.MELEE => general.model.meleeAnimationDelay,
            AttackType.RANGED => general.model.rangedAnimationDelay,
            _ => 0
        };
    
        general.animationController.AnimateAttack(general.Weapon.attackType);
        
        // TODO: Implement AudioController

        // if(general.Weapon.CanAttack)
        // {
        //     if(general.Weapon.attackType == AttackType.MELEE 
        //         || general.stateController.weaponState != WeaponState.ATTACK)
        //     {
        //         general.audioController.Play(PlayerAudioController.ATTACK_KEY);
        //     }
        // }

        TriggerWeaponState(WeaponState.ALTERNATE_ATTACK);
        yield return new WaitForSeconds(delay);
        general.Weapon.AlternateAttack();
    }

    private IEnumerator HandleAttackWindow(float attackWindow)
    {
        yield return new WaitForSeconds(attackWindow);
        general.stateController.ClearWeaponState();
    }

    private void TriggerWeaponState(WeaponState state)
    {
        general.stateController.SetWeaponState(state);
        if(attackWindowCoroutine != null)
        {
            general.StopCoroutine(attackWindowCoroutine);
        }

        float delay = state switch
        {
            WeaponState.ATTACK => general.Weapon.data.attackInterval,
            WeaponState.ALTERNATE_ATTACK => general.Weapon.data.alternateAttackInterval,
            WeaponState.SKILL => general.Weapon.data.skillInterval,
            _ => 0
        };

        attackWindowCoroutine = general.StartCoroutine(HandleAttackWindow(delay + attackWindowSize));
    }

    private void OnDamaged()
    {
        nav.velocity /= 2;
    }

    private void OnDeath()
    {
        GoToward(general.transform);
        nav.velocity = Vector3.zero;
    }
}