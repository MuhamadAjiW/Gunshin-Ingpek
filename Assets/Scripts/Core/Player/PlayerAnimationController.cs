using UnityEngine;

public class PlayerAnimationController : AnimationController{
    // Consts
    private const string IDLE_TRIGGER = "idle_param"; 
    private const string WALK_TRIGGER = "walk_param"; 
    private const string SPRINT_TRIGGER = "sprint_param"; 
    
    // Attributes
    private readonly Player player;
    private readonly PlayerStateController playerStateController;

    // Constructor
    public PlayerAnimationController(Player player){
        this.player = player;
        model = player.transform.Find("Model");
        animator = model.GetComponent<Animator>();
        meshRenderer = model.GetComponent<MeshRenderer>();

        player.stateController.OnStateChangeEvent += AnimateStates;
    }

    // Functions
    public void AnimateStates(){
        switch (player.stateController.state){
            case PlayerState.IDLE:
                animator.SetBool(IDLE_TRIGGER, true);
                break;
            case PlayerState.WALKING:
                animator.SetBool(WALK_TRIGGER, true);
                break;
            case PlayerState.SPRINTING:
                animator.SetBool(SPRINT_TRIGGER, true);
                break;
        }
    }
}
