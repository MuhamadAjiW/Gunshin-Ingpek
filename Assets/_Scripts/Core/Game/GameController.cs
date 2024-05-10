using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Static Instance
    public static GameController Instance;

    // Attributes
    public Player player;
    public GameCameraController mainCamera;
    public GameStateController stateController;
    public GameCheatController cheatController;

    // Cheat Attributes
    private float cheatDelayTimer;
    private const float cheatAllowedDelay = 1f;
    private int cheatTriggerIdx;

    // Events
    public event Action<string, System.Object> OnGameEvent;

    // Set-Getters
    public bool IsPaused => Time.timeScale == 0;

    // Constructor
    GameController()
    {
        stateController = new();
        cheatController = new();
    }

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
    }

    public void OnEnable()
    {
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
                // Debug.Log("Key pressed: " + Input.inputString);
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

    private void ResetCheatInput()
    {
        cheatTriggerIdx = 0;
        cheatDelayTimer = 0f;
    }

    public void StartCutscene(string eventCode)
    {
        CutsceneData cutscene = Resources.Load<CutsceneData>(StoryConfig.CUTSCENES[eventCode]);
        DialogController.Instance.StartCutscene(cutscene);
    }

    public void StartCutscene(string eventCode, Action callback)
    {
        CutsceneData cutscene = Resources.Load<CutsceneData>(StoryConfig.CUTSCENES[eventCode]);
        DialogController.Instance.StartCutscene(cutscene, callback);
    }

    // TODO: Refactor? issabit a bit barbaric, no?
    public void InvokeEvent(string eventName, System.Object AdditionalData = null)
    {
        OnGameEvent?.Invoke(eventName, AdditionalData);
    }
}