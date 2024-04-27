using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    // Consts
    private const string IDLE_TRIGGER = "Idle_param"; 
    private const string WALK_TRIGGER = "Walk_param"; 
    private const string SPRINT_TRIGGER = "Sprint_param"; 
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

        // int stateUpdate = player.stateController.State - (oldState & player.stateController.State);

        // if((stateUpdate & PlayerState.IDLE) > 0)
        // {
        //     ClearMovementBool();
        //     animator.SetBool(IDLE_TRIGGER, true);
        // }
        // if((stateUpdate & PlayerState.WALKING) > 0)
        // {
        //     ClearMovementBool();
        //     animator.SetBool(WALK_TRIGGER, true);
        // }
        // if((stateUpdate & PlayerState.SPRINTING) > 0)
        // {
        //     ClearMovementBool();
        //     animator.SetBool(SPRINT_TRIGGER, true);
        // }
        // if((stateUpdate & PlayerState.JUMPING) > 0)
        // {
        //     ClearMovementBool();
        //     animator.SetBool(JUMP_TRIGGER, true);
        // }
        // if((stateUpdate & PlayerState.ATTACK_MELEE) > 0)
        // {
        //     ClearAttackBool();
        //     animator.SetBool(MELEE_TRIGGER, true);
        // }
        // if((stateUpdate & PlayerState.ATTACK_RANGED) > 0)
        // {
        //     ClearAttackBool();
        //     animator.SetBool(RANGED_TRIGGER, true);
        // }


        // if((player.stateController.State & PlayerState.IDLE) > 0)
        // {
        //     ClearMovementBool();
        //     animator.SetBool(IDLE_TRIGGER, true);
        // }
        // if((player.stateController.State & PlayerState.WALKING) > 0)
        // {
        //     ClearMovementBool();
        //     animator.SetBool(WALK_TRIGGER, true);
        // }
        // if((player.stateController.State & PlayerState.SPRINTING) > 0)
        // {
        //     ClearMovementBool();
        //     animator.SetBool(SPRINT_TRIGGER, true);
        // }
        // if((player.stateController.State & PlayerState.JUMPING) > 0)
        // {
        //     ClearMovementBool();
        //     animator.SetBool(JUMP_TRIGGER, true);
        // }
        // if((player.stateController.State & PlayerState.ATTACK_MELEE) > 0)
        // {
        //     ClearAttackBool();
        //     animator.SetBool(MELEE_TRIGGER, true);
        // }
        // if((player.stateController.State & PlayerState.ATTACK_RANGED) > 0)
        // {
        //     ClearAttackBool();
        //     animator.SetBool(RANGED_TRIGGER, true);
        // }


        switch (player.stateController.State)
        {
            case PlayerState.IDLE:
                animator.SetBool(IDLE_TRIGGER, true);
                break;
            case PlayerState.WALKING:
                animator.SetBool(WALK_TRIGGER, true);
                break;
            case PlayerState.SPRINTING:
                animator.SetBool(SPRINT_TRIGGER, true);
                break;
            case PlayerState.JUMPING:
                animator.SetBool(JUMP_TRIGGER, true);
                break;
            default:
                if((player.stateController.State & PlayerState.ATTACK_MELEE) > 0)
                {
                    animator.SetBool(MELEE_TRIGGER, true);
                }
                else if((player.stateController.State & PlayerState.ATTACK_RANGED) > 0)
                {
                    animator.SetBool(RANGED_TRIGGER, true);
                }
                break;
        }
    }

    private void ClearMovementBool()
    {
        animator.SetBool(IDLE_TRIGGER, false);
        animator.SetBool(JUMP_TRIGGER, false);
        animator.SetBool(WALK_TRIGGER, false);
        animator.SetBool(SPRINT_TRIGGER, false);
    }

    private void ClearAttackBool()
    {
        animator.SetBool(RANGED_TRIGGER, false);
        animator.SetBool(MELEE_TRIGGER, false);
    }

}