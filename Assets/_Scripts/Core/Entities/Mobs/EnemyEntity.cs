using _Scripts.Core.Game.Data;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyEntity : CombatantEntity 
{
    // Attriubtes
    protected NavMeshAgent nav;
    protected Player player;

    // Functions
    new protected void Start()
    {
        base.Start();

        nav = GetComponent<NavMeshAgent>();
        player = GameController.Instance.player;

        #if STRICT
        
        if(nav == null)
        {
            Debug.LogError($"Enemy entity {name} does not have a NavMeshAgent component. How to resolve: Add one to it");
        }

        #endif

        SetLayer(EnvironmentConfig.LAYER_ENEMY);
        SetAttackLayer(EnvironmentConfig.LAYER_ENEMY_ATTACK);
        Health *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.Instance.difficulty].enemyHealthMultiplier;
        tag = EnvironmentConfig.TAG_ENEMY;
    }
}
