using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RigidObject{
    private PlayerAnimationController animationController;
    private PlayerMovementController movementController;
    public PlayerStateController stateController;
    public PlayerStats stats;

    new void Start(){
        base.Start();
        stateController = new PlayerStateController(this);
        movementController = new PlayerMovementController(this);
        animationController = new PlayerAnimationController(this);
        stats = new PlayerStats(this);
    }

    new void Update(){
        base.Update();
    }

    new void FixedUpdate(){
        base.FixedUpdate();

        stateController.DetectState();
        movementController.HandleMovement();
    }
}
