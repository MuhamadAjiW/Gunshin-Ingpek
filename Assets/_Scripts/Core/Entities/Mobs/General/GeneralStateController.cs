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
    public float attackDistance = 8f;
    public float attackCloseDistance = 2f;
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
        // Get states
        int movementState = DetectMovementState();
        int aiState = DetectAIState();
        int attackState = DetectAttackState();

        // Combine states
        state = movementState | aiState | attackState;

        // Additional state
        playerInDebuff = DetectPlayerDebuff();

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

    // State functions
    private int DetectMovementState()
    {
        if(DetectJumping())
        {
            return GeneralState.JUMPING;
        }
        else if(DetectFalling())
        {
            return GeneralState.FALLING;
        }
        else if(DetectSprinting())
        {
            return GeneralState.SPRINTING;
        }
        else
        {
            return GeneralState.IDLE;
        }
    }
    private int DetectAIState()
    {
        if(GameController.Instance.player.Dead)
        {
            general.aiController.nav.speed = general.aiController.patrolSpeed;
            return GeneralState.AI_PATROL_STATE;
        }

        if(Vector3.Distance(general.Position, GameController.Instance.player.Position) < attackCloseDistance)
        {
            return HeadGoonState.AI_IN_RANGE_CLOSE_STATE;
        }
        else if(Vector3.Distance(general.Position, GameController.Instance.player.Position) < attackDistance)
        {
            return GeneralState.AI_IN_RANGE_STATE;
        }
        else if(Vector3.Distance(general.Position, GameController.Instance.player.Position) < detectionDistance)
        {
            if(GeneralState.GetAIState(state) < GeneralState.AI_DETECTED_STATE)
            {
                general.audioController.Play(General.AUDIO_CRY_KEY);
            }
            general.aiController.nav.speed = general.Speed;
            return GeneralState.AI_DETECTED_STATE;
        }
        else
        {
            general.aiController.nav.speed = general.aiController.patrolSpeed;
            return GeneralState.AI_PATROL_STATE;
        }
    }
    private int DetectAttackState()
    {
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
        return attackState;
    }
    private bool DetectPlayerDebuff()
    {
        if(GameController.Instance.player.Dead)
        {
            return false;
        }

        return Vector3.Distance(general.Position, GameController.Instance.player.Position) < debuffDistance;
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
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(monoBehaviour.transform.position, attackCloseDistance);
    }

    public void VisualizePatrolRoute(General general)
    {
        if(general.aiController.patrolRoute.Count > 1)
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < general.aiController.patrolRoute.Count - 1; i++)
            {
                Gizmos.DrawLine(general.aiController.patrolRoute[i].position, general.aiController.patrolRoute[i + 1].position);
            }
            Gizmos.DrawLine(general.aiController.patrolRoute[^1].position, general.aiController.patrolRoute[0].position);
        }
    }
}
