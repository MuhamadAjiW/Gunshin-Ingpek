using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController
{
    // Attributes
    private readonly Player player;
    private Vector3 axisX;
    private Vector3 axisZ;
    private bool aim = false;
    private readonly float maxStairHeight = 0.5f;

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
            Vector3 vec = new(cameraForward.x, Mathf.Clamp(cameraForward.y, -CameraConfig.MAX_AIM_ANGLE+50f, CameraConfig.MAX_AIM_ANGLE+50f), cameraForward.z);
            player.transform.forward = vec;
        }

        float raycastHeight = -0.07f;
        bool stairFront = false;

        while (raycastHeight < maxStairHeight)
        {
            Vector3 stairDetectorRear = player.model.Bottom + (Vector3.up * raycastHeight);
            Vector3 stairDetectorFront = player.model.Bottom + (player.transform.forward * 0.2f) + (Vector3.up * raycastHeight);

            bool hit = Physics.Linecast(stairDetectorRear, stairDetectorFront, 1);
            if(hit)
            {
                stairFront = true;
            }
            if(!hit)
            {
                if(raycastHeight > 0)
                {
                    break;
                }
            }

            raycastHeight += 0.05f;
        }

        if(stairFront && raycastHeight < maxStairHeight)
        {
            if(inputZ != 0 || inputX != 0)
            {
                // Stair
                if(raycastHeight > 0.1)
                {
                    Vector3 newPosition = player.Position;
                    newPosition += Vector3.up * (raycastHeight + 0.05f);
                    newPosition += player.transform.forward * 0.05f;
                    player.transform.position = newPosition;
                }
                // Slope
                else
                {
                    Debug.Log("Pushing");
                    Debug.Log(player.model.Bottom);
                    Vector3 force = (player.transform.forward + Vector3.up) * 0.1f;
                    player.Rigidbody.AddForce(force, ForceMode.VelocityChange);
                }
            }
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
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).offset.z += 1.4f;
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).offset.x += 0.3f;
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).offset.y += 0.3f;
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).followingTime = 0.01f;
            }
            else
            {
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).offset.z -= 1.4f;
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).offset.x -= 0.3f;
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).offset.y -= 0.3f;
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).followingTime = CameraConfig.DEFAULT_FOLLOWING_SPEED;
            }
        }
        
    }
}
