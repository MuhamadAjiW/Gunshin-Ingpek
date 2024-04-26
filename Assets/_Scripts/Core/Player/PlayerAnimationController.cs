using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    // Consts
    private const string PLAYER_STATE_PARAM = "PlayerState_param"; 
    private const string IDLE_TRIGGER = "Idle_param"; 
    private const string WALK_TRIGGER = "Walk_param"; 
    private const string SPRINT_TRIGGER = "Sprint_param"; 
    private const string JUMP_TRIGGER = "Jump_param"; 
    private const string MELEE_ATTACK_TRIGGER = "MeleeAttack_param"; 
    private const string SHOOT_ATTACK_TRIGGER = "ShootAttack_param"; 
    private const string HIT_TRIGGER = "Hit_param"; 
    
    // Attributes
    private readonly Player player;
    private readonly PlayerStateController playerStateController;

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

        switch (player.stateController.state)
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
        }
    }
}
