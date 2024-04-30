public class DefaultEntityState 
{
    public const int NULL = 0;
    public const int IDLE = 1;
    public const int WALKING = 2;
    public const int SPRINTING = 4;
    public const int JUMPING = 8;
    public const int FALLING = 16;
    public const int DEAD = 65536;

    public static int GetMovementState(int state)
    {
        return state & (IDLE | WALKING  | SPRINTING | JUMPING);
    }
}
