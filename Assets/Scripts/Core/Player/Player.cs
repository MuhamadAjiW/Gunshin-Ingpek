using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AccompaniableCombatant {
    // Attributes
    private PlayerAnimationController animationController;
    private PlayerMovementController movementController;
    private PlayerAttackController attackController;
    public PlayerStateController stateController;
    public PlayerStats stats;

    // Constructor
    new void Start(){
        base.Start();
        Health *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].PlayerHealthMultiplier;
        
        // TODO: Review, base damage is currently done in the ObjectFactory. Might need to decide which is best
        // BaseDamage *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].PlayerDamageMultiplier;

        Weapon = GetComponentInChildren<WeaponObject>();
        stateController = new PlayerStateController(this);
        movementController = new PlayerMovementController(this);
        animationController = new PlayerAnimationController(this);
        attackController = new PlayerAttackController(this);
        stats = new PlayerStats(this);

        GameController.instance.player = this;
    }

    // Functions
    new void Update(){
        base.Update();

        attackController.HandleInputs();
    }

    new void FixedUpdate(){
        base.FixedUpdate();

        stateController.UpdateState();
        movementController.HandleMovement();
    }
}
