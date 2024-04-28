using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationController : AnimationController
{
    // Consts
    private const string MOVEMENT_PARAM = "Movement_param"; 
    private const string JUMP_BOOL = "Jump_param"; 
    private const string DEAD_Trigger = "Dead_param"; 
    private const string MELEE_TRIGGER = "MeleeAttack_param"; 
    private const string RANGED_TRIGGER = "RangedAttack_param"; 
    private const string HIT_TRIGGER = "Hit_param"; 
    private const string SKILL_TRIGGER = "Skill_param"; 
    
    // Attributes
    private readonly Player player;

    // Constructor
    public PlayerAnimationController(Player player) : base(player) 
    {
        this.player = player;
        player.stateController.OnStateChangeEvent += AnimateStates;
        player.OnGroundedEvent += ClearJumping;
        player.OnDamagedEvent += OnDamaged;
        player.OnDeathEvent += OnDeath;
    }

    // Functions
    public void AnimateStates(int oldState, int newState)
    {
        #if STRICT
        if(animator == null)
        {
            Debug.LogError($"Animated object of {player.name} does not have an animator in its model. How to resolve: add an animator to its child containing the model.cs script");
        }
        #endif

        Debug.Log(newState);
        if((newState & PlayerState.JUMPING) > 0)
        {
            animator.SetBool(JUMP_BOOL, true);
        }

        switch (PlayerState.GetMovementState(newState))
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
            case PlayerState.JUMPING:
                animator.SetInteger(MOVEMENT_PARAM, PlayerState.JUMPING);
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

    public void AnimateSkill()
    {
        animator.SetBool(RANGED_TRIGGER, false);
        animator.SetBool(MELEE_TRIGGER, false);
        animator.SetBool(SKILL_TRIGGER, true);

        GameController.Instance.mainCamera.SetCameraBehaviour(CameraBehaviourType.SKILL_1);
        (GameController.Instance.mainCamera.behaviour as CameraFollowObject).target = player.transform;
    }

    private void ClearJumping()
    {
        animator.SetBool(JUMP_BOOL, false);
    }

    private void OnDamaged()
    {
        animator.SetBool(HIT_TRIGGER, true);
    }

    private void OnDeath()
    {
        animator.SetBool(DEAD_Trigger, true);
        animator.SetInteger(MOVEMENT_PARAM, PlayerState.DEAD);
    }
}