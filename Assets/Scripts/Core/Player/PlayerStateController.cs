using UnityEngine;

public class PlayerStateController : StateController{
    // Attributes
    private Player player;

    // Contstructor
    public PlayerStateController(Player player){
        this.player = player;
    }

    // Functions
    private bool DetectJumping(){
        return !player.Grounded && player.Rigidbody.velocity.y > 0;
    }
    private bool DetectFalling(){
        return !player.Grounded && player.Rigidbody.velocity.y < 0;
    }
    private bool DetectWalking(){
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }
    private bool DetectSprinting(){
        return (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && Input.GetKey(KeyCode.LeftShift);
    }

    public override int DetectState(){
        int initialState = state;

        if(DetectJumping()){
            state = PlayerState.JUMPING;
        }
        else if(DetectFalling()){
            state = PlayerState.FALLING;
        }
        else if(DetectSprinting()){
            state = PlayerState.SPRINTING;
        }
        else if(DetectWalking()){
            state = PlayerState.WALKING;
        } else{
            state = PlayerState.IDLE;
        }

        if(initialState != state) InvokeOnStateChanged();
        return state;
    }
}
