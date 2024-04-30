public class CombatantEntityState : DefaultEntityState
{
    public const int ATTACK_MELEE = 32;
    public const int ATTACK_RANGED = 64;

    public static int GetCombatState(int state)
    {
        return state & (ATTACK_MELEE | ATTACK_RANGED);
    }
}
