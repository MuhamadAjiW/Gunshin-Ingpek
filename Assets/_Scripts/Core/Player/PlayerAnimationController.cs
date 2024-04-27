using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    // Consts
    private const string MOVEMENT_PARAM = "Movement_param"; 
    private const string JUMP_TRIGGER = "Jump_param"; 
    private const string MELEE_TRIGGER = "MeleeAttack_param"; 
    private const string RANGED_TRIGGER = "RangedAttack_param"; 
    private const string HIT_TRIGGER = "Hit_param"; 
    
    // Attributes
    private readonly Player player;

    // Constructor
    public PlayerAnimationController(Player player) : base(player) 
    {
        this.player = player;
        player.stateController.OnStateChangeEvent += AnimateStates;
        player.OnGroundedEvent += ClearJumping;
    }

    // Functions
    public void AnimateStates(int oldState)
    {
        #if STRICT
        if(animator == null)
        {
            Debug.LogError($"Animated object of {player.name} does not have an animator in its model. How to resolve: add an animator to its child containing the model.cs script");
        }
        #endif
        Debug.Log(player.stateController.State);

        if((player.stateController.State & PlayerState.JUMPING) > 0)
        {
            animator.SetBool(JUMP_TRIGGER, true);
        }

        switch (GetMovementState(player.stateController.State))
        {
            case PlayerState.IDLE:
                animator.SetInteger(MOVEMENT_PARAM, PlayerState.IDLE);
                break;
            case PlayerState.WALKING:
                animator.SetInteger(MOVEMENT_PARAM, PlayerState.WALKING);
                break;
            case PlayerState.SPRINTING:
                animator.SetInteger(MOVEMENT_PARAM, PlayerState.SPRINTING);
                break;
        }
    }

    public void AnimateAttack(AttackType type)
    {
        animator.SetBool(RANGED_TRIGGER, false);
        animator.SetBool(MELEE_TRIGGER, false);
        if(type == AttackType.MELEE)
        {
            animator.SetBool(MELEE_TRIGGER, true);
        }
        if(type == AttackType.RANGED)
        {
            animator.SetBool(RANGED_TRIGGER, true);
        }
    }

    private int GetMovementState(int state)
    {
        return state & (PlayerState.IDLE | PlayerState.WALKING  | PlayerState.SPRINTING);
    }

    private void ClearJumping()
    {
        animator.SetBool(JUMP_TRIGGER, false);
    }
}