using UnityEngine;

public class FollowRadius : MonoBehaviour
{
    private AttackingPet pet;
    SphereCollider followCollider;

    private void Start()
    {
        pet = gameObject.transform.parent.GetComponentInChildren<AttackingPet>();
        if (pet == null)
        {
            Debug.LogError("FollowRadius is not attached to AttackingPet");
        }

#if STRICT
        if (!TryGetComponent(out followCollider))
        {
            Debug.LogError("FollowRadius does not have SphereCollider");
        }
#endif
        followCollider.radius = pet.stateController.followDistance;
    }

    private void Update()
    {
        gameObject.transform.position = pet.transform.position;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.transform.parent.gameObject.TryGetComponent<EnemyEntity>(out var enemy))
        {
            pet.stateController.followEnemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.transform.parent.gameObject.TryGetComponent<EnemyEntity>(out var enemy))
        {
            pet.stateController.followEnemies.Remove(enemy);
        }
    }
}