public class GeneralAnimationController : AnimationController
{
    // Consts
    private const string MOVEMENT_PARAM = "Movement_param"; 
    private const string JUMP_BOOL = "Jump_param"; 
    private const string DEAD_Trigger = "Dead_param"; 
    private const string MELEE_TRIGGER = "MeleeAttack_param"; 
    private const string RANGED_TRIGGER = "RangedAttack_param"; 
    private const string HIT_TRIGGER = "Hit_param"; 

    // Attributes
    private readonly General general;

    public GeneralAnimationController(General general) : base(general)
    {
        this.general = general;
        general.stateController.OnStateChangeEvent += AnimateStates;
        general.OnDamagedEvent += OnDamaged;
        general.OnDeathEvent += OnDeath;
    }

    // Functions
    public void AnimateStates(int oldState, int newState)
    {
        if((newState & GeneralState.JUMPING) > 0)
        {
            animator.SetBool(JUMP_BOOL, true);
        }

        switch (GeneralState.GetMovementState(newState))
        {
            case GeneralState.IDLE:
                animator.SetInteger(MOVEMENT_PARAM, GeneralState.IDLE);
                break;
            case GeneralState.SPRINTING:
                animator.SetInteger(MOVEMENT_PARAM, GeneralState.SPRINTING);
                break;
            case GeneralState.JUMPING:
                animator.SetInteger(MOVEMENT_PARAM, GeneralState.JUMPING);
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
        animator.SetInteger(MOVEMENT_PARAM, GeneralState.DEAD);
    }
}