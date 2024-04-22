public abstract class EnemyEntity : CombatantEntity 
{
    // Set-Getters
    public new string AttackLayerCode => EnvironmentConfig.LAYER_ENEMY_ATTACK;
    public new float AttackMultiplier => GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].EnemyDamageMultiplier;

    // Functions
    new protected void Start()
    {
        base.Start();
        Health *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].EnemyHealthMultiplier;
        BaseDamage *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].EnemyDamageMultiplier;
        tag = EnvironmentConfig.TAG_ENEMY;
    }
}
