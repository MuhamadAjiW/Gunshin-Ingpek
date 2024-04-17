using UnityEngine;

public class CameraFollowObject : CameraBehaviour {
    // Attributes
    [SerializeField] public Transform target;
    [SerializeField] public float followingTime = CameraConfig.DEFAULT_FOLLOWING_SPEED;
    private Vector3 velocity = Vector3.zero;
    
    // Functions
    void LateUpdate(){
        Vector3 targetPosition = new(target.position.x, target.position.y, transform.position.z);
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followingTime);
        transform.position = newPosition;
    }
}