using System;

[Serializable]
public class GoonAnimationController : AnimationController
{
    // Consts
    private const string MOVEMENT_PARAM = "Movement_param"; 
    private const string JUMP_BOOL = "Jump_param"; 
    private const string DEAD_Trigger = "Dead_param"; 
    private const string MELEE_TRIGGER = "MeleeAttack_param"; 
    private const string RANGED_TRIGGER = "RangedAttack_param"; 
    private const string HIT_TRIGGER = "Hit_param"; 

    // Attributes
    private Goon goon;

    public void Init(Goon goon)
    {
        base.Init(goon);
        this.goon = goon;
        goon.stateController.OnStateChangeEvent += AnimateStates;
        goon.OnDamagedEvent += OnDamaged;
        goon.OnDeathEvent += OnDeath;
    }

    // Functions
    public void AnimateStates(int oldState, int newState)
    {
        if((newState & GoonState.JUMPING) > 0)
        {
            animator.SetBool(JUMP_BOOL, true);
        }

        switch (GoonState.GetMovementState(newState))
        {
            case GoonState.IDLE:
                animator.SetInteger(MOVEMENT_PARAM, GoonState.IDLE);
                break;
            case GoonState.SPRINTING:
                animator.SetInteger(MOVEMENT_PARAM, GoonState.SPRINTING);
                break;
            case GoonState.JUMPING:
                animator.SetInteger(MOVEMENT_PARAM, GoonState.JUMPING);
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
        animator.SetInteger(MOVEMENT_PARAM, GoonState.DEAD);
    }
}