using UnityEngine;

public class CameraMouse : CameraFollowObject 
{
    // Attributes
    public float mouseSensitivity = 1f;
    private Vector2 mouseTurn;
    private Quaternion initialRotation;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    // Constructor
    protected new void Start()
    {
        base.Start();
        Cursor.lockState = CursorLockMode.Locked;
        initialRotation = transform.rotation;
    }

    // Functions
    protected void Update()
    {
        if(GameController.instance.IsPaused)
        {
            return;
        }

        mouseTurn.x += Input.GetAxis("Mouse X");
        mouseTurn.y += Input.GetAxis("Mouse Y");

        mouseTurn.y = Mathf.Clamp(mouseTurn.y, -GameConfig.CAMERA_MOUSE_VERTICAL_MAX, GameConfig.CAMERA_MOUSE_VERTICAL_MAX);

        Quaternion rotation = initialRotation;
        rotation = Quaternion.AngleAxis(-mouseTurn.y, Vector3.right) * rotation;
        rotation = Quaternion.AngleAxis(mouseTurn.x, Vector3.up) * rotation;


        targetPosition = target.position + rotation * offset;
        bool hit = Physics.Linecast(target.position, targetPosition, out RaycastHit hitLocation, 1);
        if (hit)
        {
            targetPosition = hitLocation.point;
        }

        targetRotation = rotation;
    }

    protected new void FixedUpdate()
    {
        transform.forward = offset.normalized;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followingTime);
        transform.localRotation = targetRotation;
        return;
    }
}


