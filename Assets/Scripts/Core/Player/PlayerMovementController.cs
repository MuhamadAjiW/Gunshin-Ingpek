using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController{
    private Player player;

    public PlayerMovementController(Player player){
        this.player = player;
    }

    private void HandleRotation(Vector3 moveDirection){
        Quaternion target = Quaternion.LookRotation(moveDirection, Vector3.up);

        // Note: this can be smoothened, for better or for worse
        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, target, GameConfig.ROTATION_SMOOTHING * Time.deltaTime);
    }

    public void HandleMovement(){
        float keyPressAD = Input.GetAxisRaw("Horizontal");
        float keyPressWS = Input.GetAxisRaw("Vertical");
        Vector3 velocity = new(player.Rigidbody.velocity.x, player.Rigidbody.velocity.y, player.Rigidbody.velocity.z);
        Vector3 dampVelocity = Vector3.zero;

        Vector3 inputVector = new(keyPressAD, 0, keyPressWS);
        Vector3 modifierVector = inputVector.normalized * player.stats.MaxSpeed;
        velocity.x = modifierVector.x;
        velocity.z = modifierVector.z;

        player.Rigidbody.velocity = Vector3.SmoothDamp(player.Rigidbody.velocity, velocity, ref dampVelocity, GameConfig.MOVEMENT_SMOOTHING);
        if(inputVector != Vector3.zero) HandleRotation(inputVector);
    }
}
