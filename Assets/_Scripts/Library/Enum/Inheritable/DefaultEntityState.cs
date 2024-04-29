public class DefaultEntityState 
{
    public const int NULL = 0;
    public const int IDLE = 1;
    public const int WALKING = 2;
    public const int SPRINTING = 4;
    public const int ATTACKING = 8;
    public const int JUMPING = 16;
    public const int FALLING = 32;
    public const int DEAD = 65536;

    public static int GetMovementState(int state)
    {
        return state & (PlayerState.IDLE | PlayerState.WALKING  | PlayerState.SPRINTING | PlayerState.JUMPING);
    }
}
