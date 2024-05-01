using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    // Attributes
    private Player player;
    public float sprintModifier = 1.5f;
    [HideInInspector] public float snapshotSpeed = 0;

    // Set-Getters
    public float MaxSpeed => PlayerState.GetMovementState(player.stateController.State) switch
    {
        PlayerState.WALKING => player.Speed,
        PlayerState.SPRINTING => player.Speed * sprintModifier,
        PlayerState.JUMPING => snapshotSpeed,
        PlayerState.FALLING => snapshotSpeed,
        _ => 0
    };

    // Constructor
    public void Init(Player player)
    {
        this.player = player;
    }
}
