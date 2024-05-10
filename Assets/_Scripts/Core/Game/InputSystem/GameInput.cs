using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    // Static Instance
    public static GameInput Instance;
    public InputSystem inputSystem;
    
    // Attributes
    // _TODO: Clear listeners
    [HideInInspector] public InputAction MoveAction;
    [HideInInspector] public InputAction LookAction;
    [HideInInspector] public InputAction FireAction;
    [HideInInspector] public InputAction AlternateFireAction;
    [HideInInspector] public InputAction JumpAction;
    [HideInInspector] public InputAction WeaponSwitchAction;
    [HideInInspector] public InputAction SkillAction;
    [HideInInspector] public InputAction SprintAction;
    [HideInInspector] public InputAction InteractAction;
    [HideInInspector] public InputAction AimAction;
    [HideInInspector] public InputAction AnyAction;
    [HideInInspector] public InputAction CancelAction;
    [HideInInspector] public InputAction Cheat1Action;
    [HideInInspector] public InputAction Cheat2Action;

    private Dictionary<InputAction, List<Action<InputAction.CallbackContext>>> callbacks;

    // Constructor
    protected void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        inputSystem = new InputSystem();
        Instance = this;
        DontDestroyOnLoad(gameObject);

        MoveAction = inputSystem.Player.Move;
        LookAction = inputSystem.Player.Look;
        FireAction = inputSystem.Player.Fire;
        AlternateFireAction = inputSystem.Player.AlternateFire;
        JumpAction = inputSystem.Player.Jump;
        WeaponSwitchAction = inputSystem.Player.WeaponSwitch;
        SkillAction = inputSystem.Player.Skill;
        SprintAction = inputSystem.Player.Sprint;
        InteractAction = inputSystem.Player.Interact;
        AimAction = inputSystem.Player.Aim;
        AnyAction = inputSystem.Player.Any;
        CancelAction = inputSystem.Player.Cancel;
        Cheat1Action = inputSystem.Player.Cheat1;
        Cheat2Action = inputSystem.Player.Cheat2;

        callbacks = new()
        {
            {MoveAction, new()},
            {LookAction, new()},
            {FireAction, new()},
            {AlternateFireAction, new()},
            {JumpAction, new()},
            {WeaponSwitchAction, new()},
            {SkillAction, new()},
            {SprintAction, new()},
            {InteractAction, new()},
            {AimAction, new()},
            {AnyAction, new()},
            {CancelAction, new()},
            {Cheat1Action, new()},
            {Cheat2Action, new()},
        };

        // _TODO: Delete, should be inside main menu
        EnableInputs();
    }

    public void EnableInputs()
    {
        inputSystem.Player.Move.Enable();
        inputSystem.Player.Look.Enable();
        inputSystem.Player.Fire.Enable();
        inputSystem.Player.AlternateFire.Enable();
        inputSystem.Player.Jump.Enable();
        inputSystem.Player.WeaponSwitch.Enable();
        inputSystem.Player.Skill.Enable();
        inputSystem.Player.Sprint.Enable();
        inputSystem.Player.Interact.Enable();
        inputSystem.Player.Aim.Enable();
        inputSystem.Player.Any.Enable();
        inputSystem.Player.Cancel.Enable();
        inputSystem.Player.Cheat1.Enable();
        inputSystem.Player.Cheat2.Enable();
    }

    public void DisableInputs()
    {
        inputSystem.Player.Move.Disable();
        inputSystem.Player.Look.Disable();
        inputSystem.Player.Fire.Disable();
        inputSystem.Player.AlternateFire.Disable();
        inputSystem.Player.Jump.Disable();
        inputSystem.Player.WeaponSwitch.Disable();
        inputSystem.Player.Skill.Disable();
        inputSystem.Player.Sprint.Disable();
        inputSystem.Player.Interact.Disable();
        inputSystem.Player.Aim.Enable();
        inputSystem.Player.Any.Disable();
        inputSystem.Player.Cancel.Disable();
        inputSystem.Player.Cheat1.Disable();
        inputSystem.Player.Cheat2.Disable();
    }

    public void ClearListeners()
    {
        foreach (InputAction inputAction in callbacks.Keys)
        {
            foreach (Action<InputAction.CallbackContext> action in callbacks[inputAction])
            {
                inputAction.performed -= action;
            }
        }
    }

    public void BindCallback(InputAction input, Action<InputAction.CallbackContext> callback)
    {
        callbacks[input].Add(callback);
        input.performed += callback;
    }
}
