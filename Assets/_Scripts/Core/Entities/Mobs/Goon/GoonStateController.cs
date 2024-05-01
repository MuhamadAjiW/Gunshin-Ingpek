using System;
using UnityEngine;

// TODO: Refactor a bit as EnemyStateController
// Lotsa codes are reusable
[Serializable]
public class GoonStateController : EntityStateController
{
    // Attributes
    [SerializeField] private Goon goon;
    public float detectionDistance = 10f;
    public float attackDistance = 1.8f;
    public WeaponState weaponState = WeaponState.IDLE;

    // Constructor
    public GoonStateController(Goon goon)
    {
        this.goon = goon;
        goon.OnDeathEvent += OnDeath;
    }

    // Functions
    protected override int DetectState()
    {
        if(goon.Dead)
        {
            return GoonState.DEAD;
        }

        // Get movementState
        int movementState = 0; 
        if(DetectJumping())
        {
            movementState = GoonState.JUMPING;
        }
        else if(DetectFalling())
        {
            movementState = GoonState.FALLING;
        }
        else if(DetectSprinting())
        {
            movementState = GoonState.SPRINTING;
        }
        else
        {
            movementState = GoonState.IDLE;
        }

        // Get aiState
        int aiState = 0;
        if(Vector3.Distance(goon.Position, GameController.Instance.player.Position) < attackDistance)
        {
            aiState = GoonState.AI_IN_RANGE_STATE;
        }
        else if(Vector3.Distance(goon.Position, GameController.Instance.player.Position) < detectionDistance)
        {
            aiState = GoonState.AI_DETECTED_STATE;
        }

        // Get attackState
        int attackState = 0;
        if(DetectAttacking())
        {
            AttackType attackType = weaponState switch
            {
                WeaponState.ATTACK => goon.Weapon.attackType,
                WeaponState.ALTERNATE_ATTACK => goon.Weapon.alternateAttackType,
                _ => AttackType.NULL
            };

            attackState = attackType switch
            {
                AttackType.RANGED => GoonState.ATTACK_RANGED,
                AttackType.MELEE => GoonState.ATTACK_MELEE,
                _ => GoonState.NULL
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
        return goon.aiController.nav.velocity.magnitude > 0.1;
    }
    private bool DetectJumping()
    {
        return !goon.Grounded && goon.Rigidbody.velocity.y > 0;
    }
    private bool DetectFalling()
    {
        return !goon.Grounded && goon.Rigidbody.velocity.y < 0;
    }
    private bool DetectAttacking()
    {
        return !goon.Weapon.CanAttack;
    }
    private void OnDeath()
    {
        state = GoonState.DEAD;
    }


    // Debugging purposes
    public void VisualizeGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(goon.transform.position, detectionDistance);
        Gizmos.DrawWireSphere(goon.transform.position, attackDistance);
    }
}
