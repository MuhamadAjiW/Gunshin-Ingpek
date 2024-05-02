public class KingState : CombatantEntityState
{
    public const int AI_PATROL_STATE = 128;
    public const int AI_DETECTED_STATE = 256;
    public const int AI_IN_RANGE_STATE = 512;
    public const int AI_IN_RANGE_CLOSE_STATE = 1024;

    public static int GetAIState(int state)
    {
        return state & (AI_PATROL_STATE | AI_DETECTED_STATE | AI_IN_RANGE_STATE  | AI_IN_RANGE_CLOSE_STATE);
    }
}