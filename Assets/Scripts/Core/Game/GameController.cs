using System;
using UnityEngine;

public class GameController : MonoBehaviour {
    // Attributes
    public static GameController instance;
    public static GameCameraController mainCamera;
    public static GameStateController stateController;
    public bool IsPaused => Time.timeScale == 0;

    // Constructor
    private void Awake(){
        if(instance == null) instance = this;
        mainCamera = new GameCameraController(GetComponentInChildren<Camera>());
        stateController = new GameStateController();
    }

    // Functions
    void Update(){
         if(Input.GetKeyDown(KeyCode.Escape)){
            stateController.HandleEscape();
         }
    }
}