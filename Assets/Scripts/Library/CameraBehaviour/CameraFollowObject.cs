using UnityEngine;

public class CameraFollowObject : CameraBehaviour {
    // Attributes
    public Transform target;
    public float followingTime = CameraConfig.DEFAULT_FOLLOWING_SPEED;
    public Vector3 offset = CameraConfig.DEFAULT_CAMERA_OFFSET;
    private Vector3 velocity = Vector3.zero;
    
    // Functions
    protected void FixedUpdate(){
        Vector3 targetPosition = target.position + offset;
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followingTime);
        transform.position = newPosition;
    }
}
