using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RigidObject{
    private PlayerAnimationController animationController;
    private PlayerMovementController movementController;
    private PlayerStateController stateController;
    public PlayerStats stats;
    public int State => stateController.state;

    new void Start(){
        base.Start();
        animationController = new PlayerAnimationController(this);
        movementController = new PlayerMovementController(this);
        stateController = new PlayerStateController(this);
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
