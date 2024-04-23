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
    public GameSaveData data;

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
        data = GameSaveData.instance;
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
