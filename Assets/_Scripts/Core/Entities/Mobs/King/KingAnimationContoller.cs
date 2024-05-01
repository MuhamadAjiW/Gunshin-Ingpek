using System;

[Serializable]
public class KingAnimationController : AnimationController
{
    // Consts
    private const string MOVEMENT_PARAM = "Movement_param"; 
    private const string JUMP_BOOL = "Jump_param"; 
    private const string DEAD_Trigger = "Dead_param"; 
    private const string MELEE_TRIGGER = "MeleeAttack_param"; 
    private const string RANGED_TRIGGER = "RangedAttack_param"; 
    private const string HIT_TRIGGER = "Hit_param"; 

    // Attributes
    private King king;

    public void Init(King king)
    {
        base.Init(king);
        this.king = king;
        king.stateController.OnStateChangeEvent += AnimateStates;
        king.OnDamagedEvent += OnDamaged;
        king.OnDeathEvent += OnDeath;
    }

    // Functions
    public void AnimateStates(int oldState, int newState)
    {
        if((newState & KingState.JUMPING) > 0)
        {
            animator.SetBool(JUMP_BOOL, true);
        }

        switch (KingState.GetMovementState(newState))
        {
            case KingState.IDLE:
                animator.SetInteger(MOVEMENT_PARAM, KingState.IDLE);
                break;
            case KingState.SPRINTING:
                animator.SetInteger(MOVEMENT_PARAM, KingState.SPRINTING);
                break;
            case KingState.JUMPING:
                animator.SetInteger(MOVEMENT_PARAM, KingState.JUMPING);
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

    private void OnDamaged()
    {
        animator.SetBool(HIT_TRIGGER, true);
    }

    private void OnDeath()
    {
        animator.SetBool(DEAD_Trigger, true);
        animator.SetInteger(MOVEMENT_PARAM, KingState.DEAD);
    }
}