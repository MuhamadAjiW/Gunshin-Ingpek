using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    // Attributes
    private Player player;
    public float sprintModifier = 1.5f;
    public List<float> speedEffects;
    public List<float> damageEffects;
    [HideInInspector] public float snapshotSpeed = 0;

    // Set-Getters
    public float Health
    {
        get => player.Health;
        set => player.Health = value;
    }
    public float MaxHealth 
    {
        get => player.MaxHealth;
        set => player.MaxHealth = value;
    }
    public float BaseDamage
    {
        get => player.BaseDamage;
        set => player.BaseDamage = value;
    }
    public float MaxSpeed => PlayerState.GetMovementState(player.stateController.State) switch
    {
        PlayerState.WALKING => player.BaseSpeed,
        PlayerState.SPRINTING => player.BaseSpeed * sprintModifier,
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
