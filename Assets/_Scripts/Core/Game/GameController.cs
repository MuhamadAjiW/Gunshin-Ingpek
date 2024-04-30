using System;
using System.Collections;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Static Instance
    public static GameController Instance;

    // Attributes
    public Player player;
    public GameCameraController mainCamera;
    public GameStateController stateController;

    // Cheat Attributes
    private float cheatDelayTimer;
    private const float cheatAllowedDelay = 1f;
    private int cheatTriggerIdx;

    // Set-Getters
    public bool IsPaused => Time.timeScale == 0;

    // Constructor
    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        mainCamera = new GameCameraController(GetComponentInChildren<Camera>());

#if STRICT
        if (mainCamera == null)
        {
            Debug.LogError("No main camera detected in child of GameController. How to resolve: create a camera object as child of GameController");
        }
#endif
        stateController = new GameStateController();
    }

    // Functions
    protected void Update()
    {
        if (Input.GetKeyDown(GameInput.Instance.backButton))
        {
            stateController.HandleEscape();
        }

        if (stateController.GetState() == GameState.RUNNING)
        {
            // only open cheat console if game is running
            cheatDelayTimer += Time.deltaTime;
            if (cheatDelayTimer > cheatAllowedDelay)
            {
                ResetCheatInput();
            }

            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(GameInput.Instance.cheatTriggerButton[cheatTriggerIdx]))
                {
                    cheatTriggerIdx++;
                    cheatDelayTimer = 0f;
                }
                else
                {
                    ResetCheatInput();
                }
            }

            if (cheatTriggerIdx == GameInput.Instance.cheatTriggerButton.Count)
            {
                stateController.PushState(GameState.CHEAT);
                ResetCheatInput();
            }
        }
    }

    void ResetCheatInput()
    {
        cheatTriggerIdx = 0;
        cheatDelayTimer = 0f;
    }
}