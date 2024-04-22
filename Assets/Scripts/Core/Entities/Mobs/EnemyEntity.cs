public abstract class EnemyEntity : CombatantEntity 
{
    // Functions
    new protected void Start()
    {
        base.Start();
        Health *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].EnemyHealthMultiplier;
        BaseDamage *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].EnemyDamageMultiplier;
        tag = GameEnvironmentConfig.TAG_ENEMY;
    }
}
