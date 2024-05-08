using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Attributes
    private int SpawnedCount = 0;
    public int SpawnedCountLimit = 3;
    public float SpawnDelay = 15;
    public List<String> PrefabPaths = new(){
        "Prefabs/Mobs/Goon/Goon",
        "Prefabs/Mobs/Goon/Goon_Rifle"
    };
    
    // Constructor
    protected void Start()
    {
        StartCoroutine(SpawnWrapper());
    }

    // Functions
    protected IEnumerator SpawnWrapper()
    {
        float rng = UnityEngine.Random.Range(0, SpawnDelay);
        yield return new WaitForSeconds(rng);
        StartCoroutine(Spawn());
    }

    protected IEnumerator Spawn()
    {
        if(SpawnedCount < SpawnedCountLimit)
        {
            int rng = UnityEngine.Random.Range(0, PrefabPaths.Count);
            
            EnemyEntity spawnedEnemy;
            if(rng < PrefabPaths.Count)
            {
                Debug.Log(transform.position);
                spawnedEnemy = ObjectFactory.CreateEntity<EnemyEntity>(
                    prefabPath: PrefabPaths[rng],
                    position: transform.position + transform.up - transform.right + transform.forward,
                    objectName: "Monster"
                );
            }
        }
        yield return new WaitForSeconds(SpawnDelay); 
        StartCoroutine(Spawn());
    }

    protected void OnMonsterDeath()
    {
        SpawnedCount--;
    }
}
