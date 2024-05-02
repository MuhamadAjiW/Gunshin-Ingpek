using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// TODO: Refactor a bit as EnemyAIController
// Lotsa codes are reusable
[Serializable]
public class KingAIController
{
    // Attributes
    [HideInInspector] public NavMeshAgent nav;
    private King king;
    protected float attackWindowSize = 0.3f;
    protected Coroutine attackWindowCoroutine;
    protected int patrolIndex = 0;
    public float patrolSpeed = 3;
    public List<Transform> patrolRoute;

    // Constructor
    public void Init(King king)
    {
        this.king = king;
        nav = king.GetComponent<NavMeshAgent>();
        king.OnDamagedEvent += OnDamaged;
        king.OnDeathEvent += OnDeath;
    }

    // Functions
    public void Action()
    {
        switch (KingState.GetAIState(king.stateController.State))
        {
            case KingState.AI_PATROL_STATE:
                if(patrolRoute.Count > 0)
                {
                    GoToward(patrolRoute[patrolIndex]);
                    if(Vector3.Distance(patrolRoute[patrolIndex].position, king.transform.position) < 0.1)
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
            case KingState.AI_DETECTED_STATE:
                GoToward(GameController.Instance.player.transform);
                break;
            case KingState.AI_IN_RANGE_STATE:
                GoToward(king.transform);
                Quaternion targetAngle = LookToward(GameController.Instance.player.transform);
                if(Quaternion.Angle(targetAngle, king.transform.rotation) < 10)
                {
                    Attack();
                }
                break;
        }
    }

    public Quaternion LookToward(Transform target)
    {
        Vector3 direction = MathUtils.GetDirectionVectorFlat(target.position + target.right / 2, king.Position);
        Quaternion look = Quaternion.LookRotation(direction);
        king.transform.rotation = Quaternion.Slerp(look, king.transform.rotation, Time.deltaTime);
        return look;
    }

    public void GoToward(Transform target)
    {
        nav.destination = target.position;
    }

    public void Attack()
    {
        if(king.Weapon == null)
        {
            return;
        }

        king.StartCoroutine(HandleAttack());
    }

    public void AlternateAttack()
    {
        if(king.Weapon == null)
        {
            return;
        }

        king.StartCoroutine(HandleAlternateAttack());
    }

    public IEnumerator HandleAttack()
    {
        float delay = king.Weapon.attackType switch
        {
            AttackType.MELEE => king.model.meleeAnimationDelay,
            AttackType.RANGED => king.model.rangedAnimationDelay,
            _ => 0
        };
    
        king.animationController.AnimateAttack(king.Weapon.attackType);
        
        if(king.Weapon.CanAttack)
        {
            if(king.Weapon.attackType == AttackType.MELEE 
                || king.stateController.weaponState != WeaponState.ATTACK)
            {
                king.audioController.Play(King.AUDIO_ATTACK_KEY);
            }
        }

        TriggerWeaponState(WeaponState.ATTACK);
        yield return new WaitForSeconds(delay);
        king.Weapon.Attack();
    }

    public IEnumerator HandleAlternateAttack()
    {
        float delay = king.Weapon.alternateAttackType switch
        {
            AttackType.MELEE => king.model.meleeAnimationDelay,
            AttackType.RANGED => king.model.rangedAnimationDelay,
            _ => 0
        };
    
        king.animationController.AnimateAttack(king.Weapon.alternateAttackType);
        
        if(king.Weapon.CanAttack)
        {
            if(king.Weapon.alternateAttackType == AttackType.MELEE 
                || king.stateController.weaponState != WeaponState.ATTACK)
            {
                king.audioController.Play(King.AUDIO_ATTACK_KEY);
            }
        }

        TriggerWeaponState(WeaponState.ALTERNATE_ATTACK);
        yield return new WaitForSeconds(delay);
        king.Weapon.AlternateAttack();
    }

    private IEnumerator HandleAttackWindow(float attackWindow)
    {
        yield return new WaitForSeconds(attackWindow);
        king.stateController.ClearWeaponState();
    }

    private void TriggerWeaponState(WeaponState state)
    {
        king.stateController.SetWeaponState(state);
        if(attackWindowCoroutine != null)
        {
            king.StopCoroutine(attackWindowCoroutine);
        }

        float delay = state switch
        {
            WeaponState.ATTACK => king.Weapon.data.attackInterval,
            WeaponState.ALTERNATE_ATTACK => king.Weapon.data.alternateAttackInterval,
            WeaponState.SKILL => king.Weapon.data.skillInterval,
            _ => 0
        };

        attackWindowCoroutine = king.StartCoroutine(HandleAttackWindow(delay + attackWindowSize));
    }

    private void OnDamaged()
    {
        nav.velocity /= 2;
    }

    private void OnDeath()
    {
        GoToward(king.transform);
        nav.velocity = Vector3.zero;
    }
}