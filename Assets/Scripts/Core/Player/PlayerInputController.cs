using UnityEngine;

public class PlayerInputController{
    private readonly Player player;

    public float movementInputX;
    public float movementInputZ;
    
    public PlayerInputController(Player player){
        this.player = player;
    }

    public void HandleInputs(){
        movementInputX = Input.GetAxisRaw("Horizontal");
        movementInputZ = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(GameControls.instance.attackButton)){
            Debug.Log("Player is attacking");

            if(player.Weapon == null) return;

            player.Weapon.Attack();
        }
        else if(Input.GetKeyDown(GameControls.instance.interactButton)){
            Debug.Log("Player is interacting");
        
            ObjectManager.instance.LogObjects();
            EntityManager.instance.LogObjects();
        }
    }
}