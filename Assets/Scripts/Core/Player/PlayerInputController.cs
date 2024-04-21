using System;
using UnityEngine;

public class PlayerInputController{
    // Attributes
    private readonly Player player;
    public float movementInputX;
    public float movementInputZ;
    
    // Constructor
    public PlayerInputController(Player player){
        this.player = player;
    }

    // Functions
    public void HandleInputs(){
        movementInputX = Input.GetAxisRaw("Horizontal");
        movementInputZ = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(GameInput.instance.attackButton)){
            Debug.Log("Player is Attacking");
            if(player.Weapon == null) return;

            player.Weapon.Attack();
        }
        else if(Input.GetKeyDown(GameInput.instance.attackAlternateButton)){
            Debug.Log("Player is Attacking (alternate)");
            if(player.Weapon == null) return;

            player.Weapon.AttackAlternate();
        }
        else if(Input.GetKeyDown(GameInput.instance.interactButton)){
            Debug.Log("Player is interacting");
            if(player.stateController.currentInteractables.Count == 0 ) return;
            IInteractable interactable = player.stateController.currentInteractables[player.stateController.currentInteractables.Count - 1];
            interactable.Interact();
        }
    }
}