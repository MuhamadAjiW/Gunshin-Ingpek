using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private float cheatTrigger1;
    private float cheatTrigger2;

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
        GameInput.Instance.BindCallback(GameInput.Instance.CancelAction, stateController.HandleEscape);
    }

    // Functions
    protected void Update()
    {
        if (stateController.GetState() == GameState.RUNNING)
        {
            if (GameInput.Instance.Cheat1Action.ReadValue<float>() > 0 &&
                GameInput.Instance.Cheat2Action.ReadValue<float>() > 0)
            {
                stateController.PushState(GameState.CHEAT);
            }
        }
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

    protected void OnDestroy() {
        GameInput.Instance.ClearListeners();
    }
}