using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerEntity 
{
    // Static Attributes
    public const string ObjectIdPrefix = "Player"; 

    // Attributes
    public PlayerMovementController movementController;
    public PlayerAnimationController animationController;
    public PlayerInputController inputController;
    public PlayerStateController stateController;
    public PlayerStats stats;

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
        SetLayer(EnvironmentConfig.LAYER_PLAYER);
        SetAttackLayer(EnvironmentConfig.LAYER_PLAYER_ATTACK);
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
    public new void EquipWeapon(int index){
        if(stateController.weaponState != WeaponState.IDLE)
        {
            return;
        }

        base.EquipWeapon(index);
        Weapon.transform.localScale = new(0.01f, 0.01f, 0.01f);
    }

    protected new void Update()
    {
        base.Update();
        if(Dead)
        {
            return;
        }

        inputController.HandleInputs();
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
        if(Dead)
        {
            return;
        }

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
