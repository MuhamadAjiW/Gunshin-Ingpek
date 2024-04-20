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

        if(Input.GetKeyDown(GameControls.instance.attackButton)){
            Debug.Log("Player is attacking");

            if(player.Weapon == null) return;

            player.Weapon.Attack();
        }
        else if(Input.GetKeyDown(GameControls.instance.attackAlternateButton)){
            Debug.Log("Player is attacking (alternate)");

            if(player.Weapon == null) return;

            player.Weapon.AttackAlternate();
        }
        else if(Input.GetKeyDown(GameControls.instance.interactButton)){
            Debug.Log("Player is interacting");
        
            ObjectManager.instance.LogObjects();
            EntityManager.instance.LogObjects();
        }
    }
}