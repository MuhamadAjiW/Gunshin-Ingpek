using UnityEngine;

public class PlayerAnimationController{
    // Consts
    private const string IDLE_TRIGGER = "idle_param"; 
    private const string WALK_TRIGGER = "walk_param"; 
    
    // Attributes
    private Player player;
    private PlayerStateController playerStateController;
    private Transform modelTransform;
    private Animator animator;

    // Constructor
    public PlayerAnimationController(Player player){
        this.player = player;
        modelTransform = player.transform.Find("Model");
        animator = modelTransform.GetComponent<Animator>();

        player.stateController.OnStateChange += Animate;
    }

    // Functions
    public void Animate(){
        switch (player.stateController.state){
            case PlayerState.IDLE:
                animator.SetBool(IDLE_TRIGGER, true);
                break;
            case PlayerState.WALKING:
                animator.SetBool(WALK_TRIGGER, true);
                break;
        }
    }
}
