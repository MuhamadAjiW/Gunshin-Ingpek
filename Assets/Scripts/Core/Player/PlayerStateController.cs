using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : DamageableEntityStateController {
    // Attributes
    private readonly Player player;
    public List<IInteractable> currentInteractables = new();

    // Contstructor
    public PlayerStateController(Player player) : base(player){
        this.player = player;
    }

    // Functions
    private bool DetectWalking(){
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }
    private bool DetectSprinting(){
        return (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && Input.GetKey(KeyCode.LeftShift);
    }

    public override int UpdateState(){
        int initialState = state;

        if(DetectSprinting()){
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
