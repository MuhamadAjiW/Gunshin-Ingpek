using System;
using UnityEngine;

// TODO: Refactor a bit as EnemyStateController
// Lotsa codes are reusable
[Serializable]
public class KingStateController : EntityStateController
{
    // Attributes
    private King king;
    public float detectionDistance = 25f;
    public float debuffDistance = 12.5f;
    public float attackDistance = 8f;
    [HideInInspector] public WeaponState weaponState = WeaponState.IDLE;
    [HideInInspector] public bool playerInDebuff = false;

    // Effects
    public event Action OnPlayerEnterDetectionEvent;
    public event Action OnPlayerEnterDebuffEvent;
    public event Action OnPlayerLeaveDebuffEvent;

    // Constructor
    public void Init(King king)
    {
        this.king = king;
        king.OnDeathEvent += OnDeath;
    }

    // Functions
    protected override int DetectState()
    {
        // Get movementState
        int movementState = 0; 
        if(DetectJumping())
        {
            movementState = KingState.JUMPING;
        }
        else if(DetectFalling())
        {
            movementState = KingState.FALLING;
        }
        else if(DetectSprinting())
        {
            movementState = KingState.SPRINTING;
        }
        else
        {
            movementState = KingState.IDLE;
        }

        // Get aiState
        int aiState = 0;
        if(Vector3.Distance(king.Position, GameController.Instance.player.Position) < attackDistance)
        {
            aiState = KingState.AI_IN_RANGE_STATE;
        }
        else if(Vector3.Distance(king.Position, GameController.Instance.player.Position) < detectionDistance)
        {
            if(KingState.GetAIState(state) < KingState.AI_DETECTED_STATE)
            {
                king.audioController.Play(King.AUDIO_CRY_KEY);
            }
            king.aiController.nav.speed = king.Speed;
            aiState = KingState.AI_DETECTED_STATE;
        }
        else
        {
            king.aiController.nav.speed = king.aiController.patrolSpeed;
            aiState = KingState.AI_PATROL_STATE;
        }

        // Get attackState
        int attackState = 0;
        if(DetectAttacking())
        {
            AttackType attackType = weaponState switch
            {
                WeaponState.ATTACK => king.Weapon.attackType,
                WeaponState.ALTERNATE_ATTACK => king.Weapon.alternateAttackType,
                _ => AttackType.NULL
            };

            attackState = attackType switch
            {
                AttackType.RANGED => KingState.ATTACK_RANGED,
                AttackType.MELEE => KingState.ATTACK_MELEE,
                _ => KingState.NULL
            };
        }

        // Combine states
        state = movementState | aiState | attackState;
        
        // Additional state
        bool previousDebuff = playerInDebuff;
        playerInDebuff = Vector3.Distance(king.Position, GameController.Instance.player.Position) < debuffDistance;
        if(playerInDebuff != previousDebuff)
        {
            if(playerInDebuff == true)
            {
                OnPlayerEnterDebuffEvent?.Invoke();
            }
            else
            {
                OnPlayerLeaveDebuffEvent?.Invoke();
            }
        }

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
        return king.aiController.nav.velocity.magnitude > 0.5;
    }
    private bool DetectJumping()
    {
        return !king.Grounded && king.Rigidbody.velocity.y > 0;
    }
    private bool DetectFalling()
    {
        return !king.Grounded && king.Rigidbody.velocity.y < 0;
    }
    private bool DetectAttacking()
    {
        return !king.Weapon.CanAttack;
    }
    private void OnDeath()
    {
        state = KingState.DEAD;
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

    public void VisualizePatrolRoute(King king)
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < king.aiController.patrolRoute.Count - 1; i++)
        {
            Gizmos.DrawLine(king.aiController.patrolRoute[i].position, king.aiController.patrolRoute[i + 1].position);
        }
        Gizmos.DrawLine(king.aiController.patrolRoute[^1].position, king.aiController.patrolRoute[0].position);
    }
}
