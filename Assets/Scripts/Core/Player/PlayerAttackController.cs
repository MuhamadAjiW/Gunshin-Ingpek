using UnityEngine;

public class PlayerAttackController{
    private readonly Player player;
    
    public PlayerAttackController(Player player){
        this.player = player;
    }

    public void HandleInputs(){
        if(Input.GetKeyDown(GameControls.instance.attackButton)){
            Debug.Log("Player is attacking");

            if(player.Weapon == null) return;

            player.Weapon.Attack();
        }
    }
}