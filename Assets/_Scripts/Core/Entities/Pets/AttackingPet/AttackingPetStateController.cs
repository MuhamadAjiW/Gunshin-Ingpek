using System;
using UnityEngine;

using System.Collections.Generic;

[Serializable]
public class AttackingPetStateController : PetStateController<AttackingPet>
{
    // Attributes
    public float followDistance = 15f;
    public float attackDistance = 7f;
    [HideInInspector] public List<EnemyEntity> attackEnemies = new();
    [HideInInspector] public List<EnemyEntity> followEnemies = new();
    public EnemyEntity nearest;
    protected LayerMask enemyLayer;

    // Constructor
    public new void Init(AttackingPet pet)
    {
        base.Init(pet);
        enemyLayer = LayerMask.GetMask(EnvironmentConfig.LAYER_ENEMY);
    }

    // Functions
    protected override int DetectState()
    {
        Collider[] attackCollider = Physics.OverlapSphere(pet.transform.position, attackDistance, enemyLayer);
        Collider[] followCollider = Physics.OverlapSphere(pet.transform.position, followDistance, enemyLayer);


        if (attackCollider.Length > 0)
        {
            nearest = GetNearestEnemyFromCollider(attackCollider);
            state = AttackingPetState.AI_ATTACK_STATE;
        }
        else if (followCollider.Length > 0)
        {
            nearest = GetNearestEnemyFromCollider(followCollider);
            state = AttackingPetState.AI_FOLLOW_STATE;
        }
        else
        {
            state = AttackingPetState.AI_IDLE_STATE;
        }

        return state;
    }

    public EnemyEntity GetNearestEnemy(bool follow)
    {
        List<EnemyEntity> enemies = follow ? followEnemies : attackEnemies;
        if (enemies.Count == 0) return null;

        EnemyEntity nearestEnemy = enemies[0];
        float nearestDistance = Vector3.Distance(pet.transform.position, nearestEnemy.transform.position);
        foreach (EnemyEntity enemy in enemies)
        {
            float distance = Vector3.Distance(pet.transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    public EnemyEntity GetNearestEnemyFromCollider(Collider[] colliders)
    {
        if (colliders.Length == 0) return null;

        EnemyEntity nearestEnemy = colliders[0].transform.parent.GetComponent<EnemyEntity>();
        float nearestDistance = Vector3.Distance(pet.transform.position, nearestEnemy.transform.position);
        foreach (Collider collider in colliders)
        {
            EnemyEntity enemy = collider.transform.parent.GetComponent<EnemyEntity>();
            float distance = Vector3.Distance(pet.transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    public void RemoveAllNullEnemies()
    {
        attackEnemies.RemoveAll(enemy => enemy == null);
        followEnemies.RemoveAll(enemy => enemy == null);
    }


    // Debugging functions
    public void VisualizeDetection(MonoBehaviour monoBehaviour)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(monoBehaviour.transform.position, followDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(monoBehaviour.transform.position, attackDistance);
    }
}
