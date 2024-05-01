using System;
using UnityEngine;

// TODO: Refactor a bit as EnemyStateController
// Lotsa codes are reusable
[Serializable]
public class GeneralStateController : EntityStateController
{
    // Attributes
    [SerializeField] private General general;
    public float detectionDistance = 15f;
    public float attackDistance = 2f;
    public WeaponState weaponState = WeaponState.IDLE;

    // Constructor
    public GeneralStateController(General general)
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
            aiState = GeneralState.AI_DETECTED_STATE;
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


    // Debugging purposes
    public void VisualizeGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(general.transform.position, detectionDistance);
        Gizmos.DrawWireSphere(general.transform.position, attackDistance);
    }
}
