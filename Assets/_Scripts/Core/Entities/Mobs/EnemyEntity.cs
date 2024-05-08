using System.Collections;
using _Scripts.Core.Game.Data;
using _Scripts.Core.Game.Data.Saves;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyEntity : CombatantEntity
{
    // Attriubtes
    protected NavMeshAgent nav;
    protected Player player;

    protected static int spawnedOrbs = 0;
    protected static int maxSpawnedOrbs = 5;
    protected const string ORB_PREFAB = "Prefabs/Collectibles/Orb/RestoreHealthOrb/RestoreHealthOrb";

    // Functions
    new protected void Start()
    {
        base.Start();

        nav = GetComponent<NavMeshAgent>();
        player = GameController.Instance.player;

#if STRICT

        if (nav == null)
        {
            Debug.LogError($"Enemy entity {name} does not have a NavMeshAgent component. How to resolve: Add one to it");
        }

#endif

        SetLayer(EnvironmentConfig.LAYER_ENEMY);
        SetAttackLayer(EnvironmentConfig.LAYER_ENEMY_ATTACK);
        Health *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveManager.Instance.GetActiveGameSave().difficulty].enemyHealthMultiplier;
        tag = EnvironmentConfig.TAG_ENEMY;

        StartCoroutine(SpawnOrbs());
    }

    protected IEnumerator SpawnOrbs()
    {
        if (spawnedOrbs < maxSpawnedOrbs && !Dead && Random.Range(0, 4) == 0) // 25% chance to spawn an orb
        {
            Debug.Log($"{name} spawned an orb");
            Orb orb = Orb.GenerateRandomOrb(transform.position - new Vector3(0, 0, 1), $"{name}'s Orb");
            orb.AddOnTimeout(() => spawnedOrbs--);
            orb.AddOnCollect(() => spawnedOrbs--);
            spawnedOrbs++;
        }

        yield return new WaitForSeconds(15);

        if (!Dead)
        {
            StartCoroutine(SpawnOrbs());
        }
    }
}
