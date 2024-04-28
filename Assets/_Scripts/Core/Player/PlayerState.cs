public class PlayerState : DefaultEntityState 
{
    public const int ATTACK_MELEE = 64;
    public const int ATTACK_RANGED = 128;

    public static int GetMovementState(int state)
    {
        return state & (PlayerState.IDLE | PlayerState.WALKING  | PlayerState.SPRINTING | PlayerState.JUMPING);
    }
}
