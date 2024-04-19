using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController{
    // Attributes
    private readonly Player player;

    // Constructor
    public PlayerMovementController(Player player){
        this.player = player;
    }

    // Functions
    private void HandleRotation(Vector3 moveDirection){
        Quaternion target = Quaternion.LookRotation(moveDirection, Vector3.up);

        // Note: this can be smoothened, for better or for worse
        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, target, GameConfig.ROTATION_SMOOTHING * Time.deltaTime);
    }

    public void HandleMovement(){
        float inputX = player.inputController.movementInputX;
        float inputZ = player.inputController.movementInputZ;
        Vector3 velocity = new(player.Rigidbody.velocity.x, player.Rigidbody.velocity.y, player.Rigidbody.velocity.z);
        Vector3 dampVelocity = Vector3.zero;

        Vector3 inputVector = new(inputX, 0, inputZ);
        Vector3 modifierVector = inputVector.normalized * player.stats.MaxSpeed;
        velocity.x = modifierVector.x;
        velocity.z = modifierVector.z;

        player.Rigidbody.velocity = Vector3.SmoothDamp(player.Rigidbody.velocity, velocity, ref dampVelocity, GameConfig.MOVEMENT_SMOOTHING);
        if(inputVector != Vector3.zero) HandleRotation(inputVector);
    }
}
