public class HeadGoonAnimationController : AnimationController
{
    // Consts
    private const string MOVEMENT_PARAM = "Movement_param"; 
    private const string JUMP_BOOL = "Jump_param"; 
    private const string DEAD_Trigger = "Dead_param"; 
    private const string MELEE_TRIGGER = "MeleeAttack_param"; 
    private const string RANGED_TRIGGER = "RangedAttack_param"; 
    private const string HIT_TRIGGER = "Hit_param"; 

    // Attributes
    private readonly HeadGoon headGoon;

    public HeadGoonAnimationController(HeadGoon headGoon) : base(headGoon)
    {
        this.headGoon = headGoon;
        headGoon.stateController.OnStateChangeEvent += AnimateStates;
        headGoon.OnDamagedEvent += OnDamaged;
        headGoon.OnDeathEvent += OnDeath;
    }

    // Functions
    public void AnimateStates(int oldState, int newState)
    {
        if((newState & HeadGoonState.JUMPING) > 0)
        {
            animator.SetBool(JUMP_BOOL, true);
        }

        switch (HeadGoonState.GetMovementState(newState))
        {
            case HeadGoonState.IDLE:
                animator.SetInteger(MOVEMENT_PARAM, HeadGoonState.IDLE);
                break;
            case HeadGoonState.SPRINTING:
                animator.SetInteger(MOVEMENT_PARAM, HeadGoonState.SPRINTING);
                break;
            case HeadGoonState.JUMPING:
                animator.SetInteger(MOVEMENT_PARAM, HeadGoonState.JUMPING);
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
        animator.SetInteger(MOVEMENT_PARAM, HeadGoonState.DEAD);
    }
}