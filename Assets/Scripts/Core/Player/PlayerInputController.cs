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
            if(player.Weapon == null) return;

            player.Weapon.Attack();
        }
        else if(Input.GetKeyDown(GameInput.instance.attackAlternateButton)){
            if(player.Weapon == null) return;

            player.Weapon.AttackAlternate();
        }
        else if(Input.GetKeyDown(GameInput.instance.interactButton)){
            ObjectManager.instance.LogObjects();
            EntityManager.instance.LogObjects();
        }
    }
}