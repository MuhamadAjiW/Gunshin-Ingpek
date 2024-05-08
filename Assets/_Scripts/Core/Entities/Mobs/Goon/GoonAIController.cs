using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// TODO: Refactor a bit as EnemyAIController
// Lotsa codes are reusable
[Serializable]
public class GoonAIController
{
    // Attributes
    private Goon goon;
    [HideInInspector] public NavMeshAgent nav;
    protected float attackWindowSize = 0.3f;
    protected Coroutine attackWindowCoroutine;
    protected int patrolIndex = 0;
    public float patrolSpeed = 3;
    public List<Transform> patrolRoute;

    // Constructor
    public void Init(Goon goon)
    {
        this.goon = goon;
        nav = goon.GetComponent<NavMeshAgent>();
        goon.OnDamagedEvent += OnDamaged;
        goon.OnDeathEvent += OnDeath;
    }

    // Functions
    public void Action()
    {
        switch (GoonState.GetAIState(goon.stateController.State))
        {
            case GoonState.AI_PATROL_STATE:
                if(patrolRoute.Count > 0)
                {
                    GoToward(patrolRoute[patrolIndex]);
                    if(Vector3.Distance(patrolRoute[patrolIndex].position, goon.transform.position) < 1)
                    {
                        if(patrolIndex < patrolRoute.Count - 1)
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
            case GoonState.AI_DETECTED_STATE:
                GoToward(GameController.Instance.player.transform);
                break;
            case GoonState.AI_IN_RANGE_STATE:
                Stop();
                Quaternion targetAngle = LookToward(GameController.Instance.player.transform);
                if(Quaternion.Angle(targetAngle, goon.transform.rotation) < 10)
                {
                    Attack();
                }
                break;
        }
    }

    public Quaternion LookToward(Transform target)
    {
        Vector3 direction = MathUtils.GetDirectionVectorClamped(target.position, goon.Position, GameConfig.CAMERA_MOUSE_VERTICAL_MAX);
        Quaternion look = Quaternion.LookRotation(direction);
        goon.transform.rotation = Quaternion.Slerp(look, goon.transform.rotation, Time.deltaTime);
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
        if(goon.Weapon == null)
        {
            return;
        }
        goon.StartCoroutine(HandleAttack());
    }

    public void AlternateAttack()
    {
        if(goon.Weapon == null)
        {
            return;
        }
        goon.StartCoroutine(HandleAlternateAttack());
    }

    public IEnumerator HandleAttack()
    {
        float delay = goon.Weapon.attackType switch
        {
            AttackType.MELEE => goon.model.meleeAnimationDelay,
            AttackType.RANGED => goon.model.rangedAnimationDelay,
            _ => 0
        };
    
        goon.animationController.AnimateAttack(goon.Weapon.attackType);
        
        // TODO: Implement AudioController

        // if(goon.Weapon.CanAttack)
        // {
        //     if(goon.Weapon.attackType == AttackType.MELEE 
        //         || goon.stateController.weaponState != WeaponState.ATTACK)
        //     {
        //         goon.audioController.Play(PlayerAudioController.ATTACK_KEY);
        //     }
        // }

        TriggerWeaponState(WeaponState.ATTACK);
        yield return new WaitForSeconds(delay);
        goon.Weapon.Attack();
    }

    public IEnumerator HandleAlternateAttack()
    {
        float delay = goon.Weapon.alternateAttackType switch
        {
            AttackType.MELEE => goon.model.meleeAnimationDelay,
            AttackType.RANGED => goon.model.rangedAnimationDelay,
            _ => 0
        };
    
        goon.animationController.AnimateAttack(goon.Weapon.alternateAttackType);
        
        // TODO: Implement AudioController

        // if(goon.Weapon.CanAttack)
        // {
        //     if(goon.Weapon.attackType == AttackType.MELEE 
        //         || goon.stateController.weaponState != WeaponState.ATTACK)
        //     {
        //         goon.audioController.Play(PlayerAudioController.ATTACK_KEY);
        //     }
        // }

        TriggerWeaponState(WeaponState.ALTERNATE_ATTACK);
        yield return new WaitForSeconds(delay);
        goon.Weapon.AlternateAttack();
    }

    private IEnumerator HandleAttackWindow(float attackWindow)
    {
        yield return new WaitForSeconds(attackWindow);
        goon.stateController.ClearWeaponState();
    }

    private void TriggerWeaponState(WeaponState state)
    {
        goon.stateController.SetWeaponState(state);
        if(attackWindowCoroutine != null)
        {
            goon.StopCoroutine(attackWindowCoroutine);
        }

        float delay = state switch
        {
            WeaponState.ATTACK => goon.Weapon.data.attackInterval,
            WeaponState.ALTERNATE_ATTACK => goon.Weapon.data.alternateAttackInterval,
            WeaponState.SKILL => goon.Weapon.data.skillInterval,
            _ => 0
        };

        attackWindowCoroutine = goon.StartCoroutine(HandleAttackWindow(delay + attackWindowSize));
    }

    private void OnDamaged()
    {
        if(nav.enabled)
        {
            nav.velocity = Vector3.zero;
        }
    }

    private void OnDeath()
    {
        Stop();
    }
}