using System;
using UnityEngine;

// TODO: Refactor a bit as EnemyStateController
// Lotsa codes are reusable
[Serializable]
public class KingStateController : EntityStateController
{
    // Attributes
    [SerializeField] private King king;
    public float detectionDistance = 25f;
    public float attackDistance = 8f;
    public WeaponState weaponState = WeaponState.IDLE;

    // Constructor
    public KingStateController(King king)
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
            aiState = KingState.AI_DETECTED_STATE;
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


    // Debugging purposes
    public void VisualizeGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(king.transform.position, detectionDistance);
        Gizmos.DrawWireSphere(king.transform.position, attackDistance);
    }
}
