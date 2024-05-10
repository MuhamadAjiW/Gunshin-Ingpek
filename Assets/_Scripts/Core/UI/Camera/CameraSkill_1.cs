using UnityEngine;

public class CameraSkill_1 : CameraFollowObject
{
    // Attributes
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private int stage = 0;

    // Constructor
    protected new void Start()
    {
        base.Start();
        setStage(0);

        Cursor.lockState = CursorLockMode.Locked;
    }

    protected new void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, targetPosition) < 0.3)
        {
            setStage(stage + 1);
        }
        else
        {
            Vector3 targetOffsetted = target.position;
            targetOffsetted.y += 0.5f;
            
            bool hit = Physics.Linecast(targetOffsetted, targetPosition, out RaycastHit hitLocation, 1);
            if (hit)
            {
                targetPosition = hitLocation.point;
            }

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followingTime * 2);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, 0.3f);
        }
        return;
    }

    private void setStage(int newStage)
    {
        stage = newStage;

        switch (stage)
        {
            case 0:
                targetPosition = target.position;
                targetPosition += target.right * CameraConfig.DEFAULT_CAMERA_OFFSET.x;
                targetPosition += target.up * CameraConfig.DEFAULT_CAMERA_OFFSET.y;
                targetPosition += target.forward * CameraConfig.DEFAULT_CAMERA_OFFSET.z;
                transform.localPosition = targetPosition;

                targetRotation = CameraConfig.DEFAULT_CAMERA_ROTATION;
                targetRotation = Quaternion.AngleAxis(target.rotation.y, target.up) * targetRotation;
                targetRotation = target.rotation;
                transform.localRotation = targetRotation;
                
                break;

            case 1:
                targetPosition = target.position;
                targetPosition += target.right * 0.5f;
                targetPosition += target.up * 0.95f;
                targetPosition += target.forward * 1.5f;

                targetRotation = Quaternion.AngleAxis(-140, target.up) * targetRotation;
                targetRotation = Quaternion.AngleAxis(-10, target.right) * targetRotation;
                targetRotation = Quaternion.AngleAxis(10, target.forward) * targetRotation;

                break;

            case 18:
                targetPosition += target.forward * 20f;

                Quaternion rotation3 = targetRotation;
                rotation3 = Quaternion.AngleAxis(-40, target.up) * rotation3;
                targetRotation = rotation3;

                break;
            case 19:
                GameController.Instance.mainCamera.SetCameraBehaviour(CameraBehaviourType.MOUSE);
                (GameController.Instance.mainCamera.behaviour as CameraFollowObject).target = GameController.Instance.player.transform;
                break;
        }
    }
}
