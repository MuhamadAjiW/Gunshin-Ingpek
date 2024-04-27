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
    public void AnimateStates()
    {
        #if STRICT
        if(animator == null)
        {
            Debug.LogError($"Animated object of {player.name} does not have an animator in its model. How to resolve: add an animator to its child containing the model.cs script");
        }
        #endif

        switch (player.stateController.State)
        {
            case PlayerState.IDLE:
                animator.SetBool(IDLE_TRIGGER, true);
                Debug.Log("Idle");
                break;
            case PlayerState.WALKING:
                animator.SetBool(WALK_TRIGGER, true);
                Debug.Log("Walking");
                break;
            case PlayerState.SPRINTING:
                animator.SetBool(SPRINT_TRIGGER, true);
                Debug.Log("Sprint");
                break;
            case PlayerState.JUMPING:
                animator.SetBool(JUMP_TRIGGER, true);
                Debug.Log("Jumping");
                break;
            default:
                if((player.stateController.State & PlayerState.ATTACK_MELEE) > 0)
                {
                    Debug.Log("Hitting");
                    animator.SetBool(MELEE_TRIGGER, true);
                }
                else if((player.stateController.State & PlayerState.ATTACK_RANGED) > 0)
                {
                    Debug.Log("Shooting");
                    animator.SetBool(RANGED_TRIGGER, true);
                }
                break;
        }
    }
}