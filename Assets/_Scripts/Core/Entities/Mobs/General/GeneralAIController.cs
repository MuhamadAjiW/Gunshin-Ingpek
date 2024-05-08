using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// TODO: Refactor a bit as EnemyAIController
// Lotsa codes are reusable
[Serializable]
public class GeneralAIController
{
    // Attributes
    private General general;
    [HideInInspector] public NavMeshAgent nav;
    protected float attackWindowSize = 0.3f;
    protected Coroutine attackWindowCoroutine;
    protected int patrolIndex = 0;
    public float patrolSpeed = 3;
    public List<Transform> patrolRoute;

    // Constructor
    public void Init(General general)
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
            case GeneralState.AI_PATROL_STATE:
                if(patrolRoute.Count > 0)
                {
                    GoToward(patrolRoute[patrolIndex]);
                    if(Vector3.Distance(patrolRoute[patrolIndex].position, general.transform.position) < 0.1)
                    {
                        if(patrolIndex < patrolRoute.Count)
                        {
                            patrolIndex++;
                        }
                        else
                        {
                            patrolIndex = 0;
                        }
                    }
                }
                break;
            case GeneralState.AI_DETECTED_STATE:
                GoToward(GameController.Instance.player.transform);
                break;
            case GeneralState.AI_IN_RANGE_STATE:
                Stop();
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
        Vector3 direction = MathUtils.GetDirectionVectorClamped(target.position, general.Position, GameConfig.CAMERA_MOUSE_VERTICAL_MAX);
        Quaternion look = Quaternion.LookRotation(direction);
        general.transform.rotation = Quaternion.Slerp(look, general.transform.rotation, Time.deltaTime);
        return look;
    }

    public void Stop()
    {
        nav.enabled = false;
    }

    public void GoToward(Transform target)
    {
        if(!nav.enabled)
        {
            nav.enabled = true;
        }
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
        
        if(general.Weapon.CanAttack)
        {
            if(general.Weapon.attackType == AttackType.MELEE 
                || general.stateController.weaponState != WeaponState.ATTACK)
            {
                general.audioController.Play(General.AUDIO_ATTACK_KEY);
            }
        }

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
    
        general.animationController.AnimateAttack(general.Weapon.alternateAttackType);
        
        if(general.Weapon.CanAttack)
        {
            if(general.Weapon.alternateAttackType == AttackType.MELEE 
                || general.stateController.weaponState != WeaponState.ATTACK)
            {
                general.audioController.Play(General.AUDIO_ATTACK_KEY);
            }
        }

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
        if(nav.enabled)
        {
            nav.velocity /= 2;
        }
    }

    private void OnDeath()
    {
        Stop();
    }
}