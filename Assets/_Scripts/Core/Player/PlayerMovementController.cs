using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController
{
    // Attributes
    private readonly Player player;
    private Vector3 axisX;
    private Vector3 axisZ;

    // Constructor
    public PlayerMovementController(Player player)
    {
        this.player = player;
        player.inputController.OnJumpEvent += HandleJump;
        axisX = new(GameController.instance.mainCamera.Orientation.right.x, 0, GameController.instance.mainCamera.Orientation.right.z);
        axisZ = new(GameController.instance.mainCamera.Orientation.forward.x, 0, GameController.instance.mainCamera.Orientation.forward.z);
    }

    // Functions
    private void HandleRotation(Vector3 moveDirection)
    {
        Quaternion target = Quaternion.LookRotation(moveDirection, Vector3.up);
        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, target, GameConfig.ROTATION_SMOOTHING * Time.deltaTime);
    }

    private void SnapshotCameraOrientation()
    {
        axisX = new(GameController.instance.mainCamera.Orientation.right.x, 0, GameController.instance.mainCamera.Orientation.right.z);
        axisZ = new(GameController.instance.mainCamera.Orientation.forward.x, 0, GameController.instance.mainCamera.Orientation.forward.z);
    }

    public void HandleMovement()
    {
        float inputX = player.inputController.movementInputX;
        float inputZ = player.inputController.movementInputZ;

        SnapshotCameraOrientation();

        Vector3 velocity = new(player.Rigidbody.velocity.x, player.Rigidbody.velocity.y, player.Rigidbody.velocity.z);
        Vector3 dampVelocity = Vector3.zero;

        Vector3 movementVector = inputX * axisX.normalized + inputZ * axisZ.normalized;
        Vector3 modifierVector = movementVector.normalized * player.stats.MaxSpeed;
        velocity.x = modifierVector.x;
        velocity.z = modifierVector.z;

        player.Rigidbody.velocity = Vector3.SmoothDamp(player.Rigidbody.velocity, velocity, ref dampVelocity, GameConfig.MOVEMENT_SMOOTHING);
        if(movementVector != Vector3.zero)
        {
            HandleRotation(movementVector);
        } 
    }

    public void HandleJump()
    {
        Vector3 force = new(0, player.JumpForce, 0);
        player.Rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
