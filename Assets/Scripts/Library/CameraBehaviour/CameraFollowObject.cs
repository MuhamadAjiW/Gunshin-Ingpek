using UnityEngine;

public class CameraFollowObject : CameraBehaviour {
    // Attributes
    [SerializeField] public Transform target;
    [SerializeField] public float followingTime = CameraConfig.DEFAULT_FOLLOWING_SPEED;
    [SerializeField] public Vector3 offset = CameraConfig.DEFAULT_CAMERA_OFFSET;
    private Vector3 velocity = Vector3.zero;
    
    // Functions
    void FixedUpdate(){
        Vector3 targetPosition = target.position + offset;
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followingTime);
        transform.position = newPosition;
    }
}