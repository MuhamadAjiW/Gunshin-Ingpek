using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameCameraController {
    // Attributes
    private Camera activeCamera;
    private CameraBehaviour behaviour;
    private CameraBehaviourType behaviourType;

    // Constructor
    public GameCameraController(Camera camera){
        activeCamera = camera;
        activeCamera.enabled = true;
    }

    // Functions
    public void SwapCamera(Camera camera){
        activeCamera.enabled = false;
        activeCamera = camera;
        activeCamera.enabled = true;
    }

    public void ResetCameraBehaviour(){
        GameObject.Destroy(activeCamera.GetComponent<CameraBehaviour>());
        behaviourType = CameraBehaviourType.NULL;
        behaviour = null;
    }

    public void SetCameraBehaviour(CameraBehaviourType cameraBehaviourType){
        GameObject.Destroy(activeCamera.GetComponent<CameraBehaviour>());

        switch (cameraBehaviourType){
            case CameraBehaviourType.STATIC:
                behaviour = activeCamera.AddComponent<CameraStatic>();                
                break;
            case CameraBehaviourType.FOLLOW:
                behaviour = activeCamera.AddComponent<CameraFollowObject>();                
                break;
            
            default:
                throw new Exception("Invalid cameraBehaviourType set, please refer to enum CameraBehaviourType for valid types");
        }
        behaviourType = cameraBehaviourType;
    }
}