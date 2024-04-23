using System;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    // Static Instance
    public static GameController instance;
    
    // Attributes
    public Player player;
    public GameCameraController mainCamera;
    public GameStateController stateController;

    // Set-getters
    public bool IsPaused => Time.timeScale == 0;

    // Constructor
    protected void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        mainCamera = new GameCameraController(GetComponentInChildren<Camera>());
        stateController = new GameStateController();
    }

    // Functions
    protected void Update()
    {
         if(Input.GetKeyDown(GameInput.instance.backButton))
         {
            stateController.HandleEscape();
         }
    }
}
