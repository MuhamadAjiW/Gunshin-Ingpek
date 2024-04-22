using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerEntity 
{
    // Static Attributes
    public const string ObjectIdPrefix = "Player"; 

    // Attributes
    private PlayerAnimationController animationController;
    private PlayerMovementController movementController;
    public PlayerInputController inputController;
    public PlayerStateController stateController;
    public PlayerStats stats;

    // Constructor
    new void Start()
    {
        base.Start();
        SetIdPrefix(ObjectIdPrefix);
        Health *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].PlayerHealthMultiplier;
        stateController = new PlayerStateController(this);
        inputController = new PlayerInputController(this);
        movementController = new PlayerMovementController(this);
        animationController = new PlayerAnimationController(this);
        stats = new PlayerStats(this);

        WeaponList.Add(GetComponentInChildren<WeaponObject>());
        
        foreach (WeaponObject weapon in GetComponentsInChildren<WeaponObject>())
        {
            WeaponList.Add(weapon);
        }
        SetWeapon(0);

        GameController.instance.player = this;
    }

    // Functions
    protected new void Update()
    {
        base.Update();

        inputController.HandleInputs();
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();

        movementController.HandleMovement();
        stateController.UpdateState();
    }

    protected void OnTriggerEnter(Collider otherCollider)
    {
        otherCollider.transform.TryGetComponent<IInteractable>(out var interactable);
        
        if(interactable == null)
        {
            return;
        }
        
        interactable.InvokeOnInteractAreaEnterEvent();
        stateController.currentInteractables.Add(interactable);
    }

    protected void OnTriggerExit(Collider otherCollider)
    {
        otherCollider.transform.TryGetComponent<IInteractable>(out var interactable);
    
        if(interactable == null)
        {
            return;
        }
    
        interactable.InvokeOnInteractAreaExitEvent();
        stateController.currentInteractables.Remove(interactable);
    }
}
