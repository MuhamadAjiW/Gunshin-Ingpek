using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AccompaniableCombatant {
    // Static attributes
    public static string ObjectIdPrefix = "Player"; 

    // Attributes
    private PlayerAnimationController animationController;
    private PlayerMovementController movementController;
    public PlayerInputController inputController;
    public PlayerStateController stateController;
    public PlayerStats stats;

    // Constructor
    new void Start(){
        base.Start();
        SetIdPrefix(ObjectIdPrefix);
        Health *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].PlayerHealthMultiplier;
        
        // TODO: Review, base damage is currently done in the ObjectFactory. Might need to decide which is best
        // BaseDamage *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].PlayerDamageMultiplier;

        Weapon = GetComponentInChildren<WeaponObject>();
        stateController = new PlayerStateController(this);
        movementController = new PlayerMovementController(this);
        animationController = new PlayerAnimationController(this);
        inputController = new PlayerInputController(this);
        stats = new PlayerStats(this);

        GameController.instance.player = this;
    }

    // Functions
    new void Update(){
        base.Update();

        inputController.HandleInputs();
    }

    new void FixedUpdate(){
        base.FixedUpdate();

        stateController.UpdateState();
        movementController.HandleMovement();
    }
}
