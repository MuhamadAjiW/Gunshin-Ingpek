using UnityEngine;

public class AttackRadius : MonoBehaviour
{
    private AttackingPet pet;
    SphereCollider attackCollider;

    private void Start()
    {
        pet = gameObject.transform.parent.GetComponentInChildren<AttackingPet>();
#if STRICT
        if (pet == null)
        {
            Debug.LogError("AttackRadius is not attached to AttackingPet");
        }
#endif

#if STRICT
        if (!TryGetComponent(out attackCollider))
        {
            Debug.LogError("AttackRadius does not have SphereCollider");
        }
#endif
        attackCollider.radius = pet.stateController.attackDistance;
    }

    private void Update()
    {
        gameObject.transform.position = pet.transform.position;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.transform.parent.gameObject.TryGetComponent<EnemyEntity>(out var enemy))
        {
            pet.stateController.attackEnemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.transform.parent.gameObject.TryGetComponent<EnemyEntity>(out var enemy))
        {
            pet.stateController.attackEnemies.Remove(enemy);
        }
    }
}