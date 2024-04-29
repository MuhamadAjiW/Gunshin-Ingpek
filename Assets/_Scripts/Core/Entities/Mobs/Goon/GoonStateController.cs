using System;
using UnityEngine;

[Serializable]
public class GoonStateController : EntityStateController
{
    // Attributes
    [SerializeField] private Goon goon;
    public static float detectionDistance = 10f;
    public static float attackDistance = 5f;

    // Constructor
    public GoonStateController(Goon goon)
    {
        this.goon = goon;
    }

    // Functions
    protected override int DetectState()
    {
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

        int aiState = 0;
        if(Vector3.Distance(goon.Position, GameController.Instance.player.Position) < attackDistance)
        {
            aiState = GoonState.AI_IN_RANGE_STATE;
        }
        else if(Vector3.Distance(goon.Position, GameController.Instance.player.Position) < detectionDistance)
        {
            aiState = GoonState.AI_DETECTED_STATE;
        }

        state = movementState | aiState;

        return state;
    }

    public void VisualizeGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(goon.transform.position, detectionDistance);
        Gizmos.DrawWireSphere(goon.transform.position, attackDistance);
    }


    private bool DetectSprinting()
    {
        return goon.Rigidbody.velocity.magnitude > 10;
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
        state = PlayerState.DEAD;
    }
}
