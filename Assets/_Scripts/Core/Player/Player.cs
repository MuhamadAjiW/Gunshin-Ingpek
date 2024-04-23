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

    // Set-Getters
    public new string AttackLayerCode => EnvironmentConfig.LAYER_PLAYER;
    public new float AttackMultiplier => GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.Instance.difficulty].playerDamageMultiplier;

    // Constructor
    new void Start()
    {
        base.Start();
        SetIdPrefix(ObjectIdPrefix);
        Health *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.Instance.difficulty].playerHealthMultiplier;
        stats = new PlayerStats(this);
        stateController = new PlayerStateController(this);
        inputController = new PlayerInputController(this);
        movementController = new PlayerMovementController(this);
        animationController = new PlayerAnimationController(this);
        GameController.Instance.player = this;


        // TODO: These are for dev, consider deleting
        WeaponList.AddRange(GetComponentsInChildren<WeaponObject>());
        EquipWeapon(0);

        int initialIndex = CompanionList.Count;
        for (int i = 0; i < initialIndex; i++)
        {            
            CompanionActive.Add(false);
        }
        
        CompanionList.AddRange(EntityManager.Instance.GetComponentsInChildren<Companion>());
        for (int i = initialIndex; i < CompanionList.Count; i++)
        {            
            CompanionActive.Add(CompanionList[i].gameObject.activeSelf);
        }
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
