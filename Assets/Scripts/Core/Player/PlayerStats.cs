public class PlayerStats {
    // Attributes
    private Player player;
    public float walkSpeed = 10;
    public float sprintSpeed = 25;
    public float jumpForce = 600;
    public float snapshotSpeed = 25;

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
}
