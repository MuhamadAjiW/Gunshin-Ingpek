using UnityEngine;

public class CameraFollowObject : CameraBehaviour {
    // Attributes
    public Transform target;
    public float followingTime = CameraConfig.DEFAULT_FOLLOWING_SPEED;
    protected Vector3 offset;
    protected Vector3 velocity;
    
    // Constructor
    protected void Start(){
        offset = transform.position - target.position;
    }

    // Functions
    protected void FixedUpdate(){
        if(GameController.instance.IsPaused) return;
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followingTime);
    }
}
