using _Scripts.Core.Game.Data;
using UnityEngine;

public abstract class EnemyEntity : CombatantEntity 
{
    // Functions
    new protected void Start()
    {
        base.Start();
        SetLayer(EnvironmentConfig.LAYER_ENEMY);
        SetAttackLayer(EnvironmentConfig.LAYER_ENEMY_ATTACK);
        Health *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.Instance.difficulty].enemyHealthMultiplier;
        tag = EnvironmentConfig.TAG_ENEMY;
    }
}
