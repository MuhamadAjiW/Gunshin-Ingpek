using System;
using UnityEngine;

// TODO: Refactor a bit as EnemyStateController
// Lotsa codes are reusable
[Serializable]
public class GoonStateController : EntityStateController
{
    // Attributes
    private Goon goon;
    public float detectionDistance = 15f;
    public float attackDistance = 1.5f;
    [HideInInspector] public WeaponState weaponState = WeaponState.IDLE;

    // Constructor
    public void Init(Goon goon)
    {
        this.goon = goon;
        goon.OnDeathEvent += OnDeath;
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
        return goon.aiController.nav.velocity.magnitude > 0.5;
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
    
    // State functions
    private int DetectMovementState()
    {
        if(DetectJumping())
        {
            return GoonState.JUMPING;
        }
        else if(DetectFalling())
        {
            return GoonState.FALLING;
        }
        else if(DetectSprinting())
        {
            return GoonState.SPRINTING;
        }
        else
        {
            return GoonState.IDLE;
        }
    }
    private int DetectAIState()
    {
        if(GameController.Instance.player.Dead)
        {
            goon.aiController.nav.speed = goon.aiController.patrolSpeed;
            return GoonState.AI_PATROL_STATE;
        }

        if(Vector3.Distance(goon.Position, GameController.Instance.player.Position) < attackDistance)
        {
            return GoonState.AI_IN_RANGE_STATE;
        }
        else if(Vector3.Distance(goon.Position, GameController.Instance.player.Position) < detectionDistance)
        {
            if(GoonState.GetAIState(state) < GoonState.AI_DETECTED_STATE)
            {
                goon.audioController.Play(Goon.AUDIO_CRY_KEY);
            }
            goon.aiController.nav.speed = goon.Speed;
            return GoonState.AI_DETECTED_STATE;
        }
        else
        {
            goon.aiController.nav.speed = goon.aiController.patrolSpeed;
            return GoonState.AI_PATROL_STATE;
        }
    }
    private int DetectAttackState()
    {
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
        return attackState;
    }


    // Debugging functions
    public void VisualizeDetection(MonoBehaviour monoBehaviour)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(monoBehaviour.transform.position, detectionDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(monoBehaviour.transform.position, attackDistance);
    }

    public void VisualizePatrolRoute(Goon goon)
    {
        if(goon.aiController.patrolRoute.Count > 1)
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < goon.aiController.patrolRoute.Count - 1; i++)
            {
                Gizmos.DrawLine(goon.aiController.patrolRoute[i].position, goon.aiController.patrolRoute[i + 1].position);
            }
            Gizmos.DrawLine(goon.aiController.patrolRoute[^1].position, goon.aiController.patrolRoute[0].position);
        }
    }
}
