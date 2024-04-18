using UnityEngine;

public class PlayerStats {
    // Attributes
    private readonly Player player;
    [SerializeField] public float walkSpeed = 10;
    [SerializeField] public float sprintSpeed = 20;
    [SerializeField] public float jumpForce = 600;
    public float snapshotSpeed = 25;
    public float Health {
        get => player.Health;
        set => player.Health = value;
    }
    public float MaxHealth {
        get => player.MaxHealth;
        set => player.MaxHealth = value;
    }
    public float BaseDamage {
        get => player.BaseDamage;
        set => player.BaseDamage = value;
    }

    public float MaxSpeed => player.stateController.state switch{
        PlayerState.WALKING => walkSpeed,
        PlayerState.SPRINTING => sprintSpeed,
        PlayerState.JUMPING => snapshotSpeed,
        PlayerState.FALLING => snapshotSpeed,
        _ => 0
    };

    // Constructor
    public PlayerStats(Player player){
        this.player = player;
    }

    // Functions
    public void SaveStats(){
        GameData.instance.Save(this);
    }
}
