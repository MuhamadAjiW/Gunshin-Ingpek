using System;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class GameCameraController
{
    // Attributes
    public Camera activeCamera;
    public CameraBehaviour behaviour;
    public CameraBehaviourType behaviourType;

    // Set-Getters
    public Transform Orientation => activeCamera.transform;

    // Constructor
    public GameCameraController(Camera camera)
    {
        activeCamera = camera;
        activeCamera.enabled = true;
        behaviour = camera.GetComponent<CameraBehaviour>();
    }

    // Functions
    public void SwapCamera(Camera camera)
    {
        activeCamera.enabled = false;
        activeCamera = camera;
        activeCamera.enabled = true;
    }

    public void ResetCameraBehaviour()
    {
        UnityEngine.GameObject.Destroy(activeCamera.GetComponent<CameraBehaviour>());
        behaviourType = CameraBehaviourType.NULL;
        behaviour = null;
    }

    public void SetCameraBehaviour(CameraBehaviourType cameraBehaviourType)
    {
        UnityEngine.GameObject.Destroy(activeCamera.GetComponent<CameraBehaviour>());

        behaviour = cameraBehaviourType switch
        {
            CameraBehaviourType.STATIC => activeCamera.AddComponent<CameraStatic>(),
            CameraBehaviourType.FOLLOW => activeCamera.AddComponent<CameraFollowObject>(),
            CameraBehaviourType.MOUSE => activeCamera.AddComponent<CameraMouse>(),
            CameraBehaviourType.SKILL_1 => activeCamera.AddComponent<CameraSkill_1>(),
            _ => throw new Exception("Invalid cameraBehaviourType set, please refer to enum CameraBehaviourType for valid types")
        };

        behaviourType = cameraBehaviourType;
    }
}