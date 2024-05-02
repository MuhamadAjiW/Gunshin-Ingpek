using System;
using UnityEngine;

// TODO: Refactor a bit as EnemyStateController
// Lotsa codes are reusable
[Serializable]
public class GeneralStateController : EntityStateController
{
    // Attributes
    private General general;
    public float detectionDistance = 20f;
    public float debuffDistance = 10f;
    public float attackDistance = 2f;
    [HideInInspector] public WeaponState weaponState = WeaponState.IDLE;
    [HideInInspector] public bool playerInDebuff = false;

    // Constructor
    public void Init(General general)
    {
        this.general = general;
        general.OnDeathEvent += OnDeath;
    }

    // Functions
    protected override int DetectState()
    {
        // Get movementState
        int movementState = 0; 
        if(DetectJumping())
        {
            movementState = GeneralState.JUMPING;
        }
        else if(DetectFalling())
        {
            movementState = GeneralState.FALLING;
        }
        else if(DetectSprinting())
        {
            movementState = GeneralState.SPRINTING;
        }
        else
        {
            movementState = GeneralState.IDLE;
        }

        // Get aiState
        int aiState = 0;
        if(Vector3.Distance(general.Position, GameController.Instance.player.Position) < attackDistance)
        {
            aiState = GeneralState.AI_IN_RANGE_STATE;
        }
        else if(Vector3.Distance(general.Position, GameController.Instance.player.Position) < detectionDistance)
        {
            if(GeneralState.GetAIState(state) < GeneralState.AI_DETECTED_STATE)
            {
                general.audioController.Play(General.AUDIO_CRY_KEY);
            }
            general.aiController.nav.speed = general.Speed;
            aiState = GeneralState.AI_DETECTED_STATE;
        }
        else
        {
            general.aiController.nav.speed = general.aiController.patrolSpeed;
            aiState = GeneralState.AI_PATROL_STATE;
        }

        // Get attackState
        int attackState = 0;
        if(DetectAttacking())
        {
            AttackType attackType = weaponState switch
            {
                WeaponState.ATTACK => general.Weapon.attackType,
                WeaponState.ALTERNATE_ATTACK => general.Weapon.alternateAttackType,
                _ => AttackType.NULL
            };

            attackState = attackType switch
            {
                AttackType.RANGED => GeneralState.ATTACK_RANGED,
                AttackType.MELEE => GeneralState.ATTACK_MELEE,
                _ => GeneralState.NULL
            };
        }

        // Combine states
        state = movementState | aiState | attackState;

        // Additional state
        playerInDebuff = Vector3.Distance(general.Position, GameController.Instance.player.Position) < debuffDistance;

        return state;
    }

    public void ClearWeaponState()
    {
        weaponState = WeaponState.IDLE;
    }
    public void SetWeaponState(WeaponState state)
    {
        weaponState = state;
    }
    private bool DetectSprinting()
    {
        return general.aiController.nav.velocity.magnitude > 0.5;
    }
    private bool DetectJumping()
    {
        return !general.Grounded && general.Rigidbody.velocity.y > 0;
    }
    private bool DetectFalling()
    {
        return !general.Grounded && general.Rigidbody.velocity.y < 0;
    }
    private bool DetectAttacking()
    {
        return !general.Weapon.CanAttack;
    }
    private void OnDeath()
    {
        state = GeneralState.DEAD;
    }


    // Debugging functions
    public void VisualizeDetection(MonoBehaviour monoBehaviour)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(monoBehaviour.transform.position, detectionDistance);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(monoBehaviour.transform.position, debuffDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(monoBehaviour.transform.position, attackDistance);
    }

    public void VisualizePatrolRoute(General general)
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < general.aiController.patrolRoute.Count - 1; i++)
        {
            Gizmos.DrawLine(general.aiController.patrolRoute[i].position, general.aiController.patrolRoute[i + 1].position);
        }
        Gizmos.DrawLine(general.aiController.patrolRoute[^1].position, general.aiController.patrolRoute[0].position);
    }
}
