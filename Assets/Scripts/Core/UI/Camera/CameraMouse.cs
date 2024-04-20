using UnityEngine;

public class CameraMouse : CameraBehaviour {
    // Attributes
    public Transform target;
    public float mouseSensitivity = 1f;
    public Vector2 mouseTurn;
    private Quaternion initialRotation;
    private Vector3 offset;
    private readonly float maximumVerticalAngle = 80f;

    protected void Start(){
        offset = transform.position - target.position;
        Cursor.lockState = CursorLockMode.Locked;
        initialRotation = transform.rotation;
    }

    protected void Update(){
        if(GameController.instance.IsPaused) return;
        transform.forward = offset.normalized;

        mouseTurn.x += Input.GetAxis("Mouse X");
        mouseTurn.y += Input.GetAxis("Mouse Y");

        mouseTurn.y = Mathf.Clamp(mouseTurn.y, -maximumVerticalAngle, maximumVerticalAngle);

        Quaternion rotation = initialRotation;
        rotation = Quaternion.AngleAxis(-mouseTurn.y, Vector3.right) * rotation;
        rotation = Quaternion.AngleAxis(mouseTurn.x, Vector3.up) * rotation;

        Vector3 newPosition = target.position + rotation * offset;

        bool hit = Physics.Linecast(target.position, newPosition, out RaycastHit hitLocation, 1);
        if (hit){
            Debug.Log("Hit Collider: " + hitLocation.collider.gameObject.name);
            transform.position = hitLocation.point;
        } else{
            transform.position = newPosition;
        }
        transform.localRotation = rotation;
    }
}
