using UnityEngine;

public class PlayerStats
{
    // Attributes
    private readonly Player player;
    public float sprintModifier = 1.5f;
    public float snapshotSpeed = 0;

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
    public float MaxSpeed => player.stateController.state switch
    {
        PlayerState.WALKING => player.BaseSpeed,
        PlayerState.SPRINTING => player.BaseSpeed * sprintModifier,
        PlayerState.JUMPING => snapshotSpeed,
        PlayerState.FALLING => snapshotSpeed,
        _ => 0
    };

    // Constructor
    public PlayerStats(Player player)
    {
        this.player = player;
    }
}
