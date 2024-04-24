using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController
{
    // Attributes
    private readonly Player player;
    private Vector3 axisX;
    private Vector3 axisZ;
    private bool aim = false;

    // Constructor
    public PlayerMovementController(Player player)
    {
        this.player = player;
        player.inputController.OnJumpEvent += HandleJump;
        player.inputController.OnAimEvent += OnAim;
        axisX = new(GameController.Instance.mainCamera.Orientation.right.x, 0, GameController.Instance.mainCamera.Orientation.right.z);
        axisZ = new(GameController.Instance.mainCamera.Orientation.forward.x, 0, GameController.Instance.mainCamera.Orientation.forward.z);
    }

    // Functions
    private void HandleRotation(Vector3 moveDirection)
    {
        Quaternion target = Quaternion.LookRotation(moveDirection, Vector3.up);
        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, target, GameConfig.ROTATION_SMOOTHING * Time.deltaTime);
    }

    private void SnapshotCameraOrientation()
    {
        axisX = new(GameController.Instance.mainCamera.Orientation.right.x, 0, GameController.Instance.mainCamera.Orientation.right.z);
        axisZ = new(GameController.Instance.mainCamera.Orientation.forward.x, 0, GameController.Instance.mainCamera.Orientation.forward.z);
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
        
        if (player.Rigidbody.velocity.y < 0)
        {
            player.Rigidbody.velocity += GameConfig.MOVEMENT_FALL_SMOOTHING * Time.deltaTime * Vector3.up;
        }

        if(movementVector != Vector3.zero)
        {
            HandleRotation(movementVector);
        }

        if (aim)
        {
            Vector3 cameraForward = GameController.Instance.mainCamera.Orientation.forward;
            Vector3 vec = new Vector3(cameraForward.x, Mathf.Clamp(cameraForward.y, -CameraConfig.MAX_AIM_ANGLE+50f, CameraConfig.MAX_AIM_ANGLE+50f), cameraForward.z);
            player.transform.forward = vec;
        }
    }

    public void HandleJump()
    {
        float snapshotSpeed = Mathf.Abs(player.Rigidbody.velocity.x * 1.1f);
        player.stats.snapshotSpeed = Mathf.Abs(snapshotSpeed > player.BaseSpeed?  snapshotSpeed : player.BaseSpeed);

        Vector3 force = new(0, player.JumpForce, 0);
        player.Rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public void OnAim(bool aim)
    {
        // Make the camera zoom in and zoom out based on the aim toggle
        this.aim = aim;
        if (GameController.Instance.mainCamera.behaviour is CameraFollowObject)
        {
            if (aim)
            {
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).offset.z += 2.2f;
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).offset.x += 0.3f;
            }
            else
            {
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).offset.z -= 2.2f;
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).offset.x -= 0.3f;
            }
        }
        
    }
}
