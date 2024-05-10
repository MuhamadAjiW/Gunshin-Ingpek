using UnityEngine;

public class CameraMouse : CameraFollowObject 
{
    
    // Attributes
    public float mouseSensitivity = 0.2f;
    private Vector2 mouseTurn;
    private Quaternion initialRotation;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector3 mouseUpAxis;
    private Vector3 mouseRightAxis;

    // Constructor
    protected new void Start()
    {
        // TODO: Can be improved by not resetting hard direction and angle wise 
        base.Start();
        offset = CameraConfig.DEFAULT_CAMERA_OFFSET;
        Cursor.lockState = CursorLockMode.Locked;
        initialRotation = target.transform.rotation;
        mouseUpAxis = target.transform.right;
        mouseRightAxis = target.transform.up;
    }

    // Functions
    protected void Update()
    {
        if(GameController.Instance.IsPaused)
        {
            return;
        }

        Vector2 mouseInput = GameInput.Instance.LookAction.ReadValue<Vector2>() * mouseSensitivity;
        mouseTurn.x += mouseInput.x;
        mouseTurn.y += mouseInput.y;

        mouseTurn.y = Mathf.Clamp(mouseTurn.y, -GameConfig.CAMERA_MOUSE_VERTICAL_MAX, GameConfig.CAMERA_MOUSE_VERTICAL_MAX);

        Quaternion rotation = initialRotation;
        rotation = Quaternion.AngleAxis(-mouseTurn.y, mouseUpAxis) * rotation;
        rotation = Quaternion.AngleAxis(mouseTurn.x, mouseRightAxis) * rotation;


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


