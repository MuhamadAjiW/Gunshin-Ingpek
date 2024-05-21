using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public abstract class PetAIController<T> where T : Companion
{
    // Attributes
    public T pet;
    [HideInInspector] public NavMeshAgent nav;
    public float defaultStoppingDistance = 1;

    // Constructor
    public void Init(T pet)
    {
        this.pet = pet;

        nav = pet.GetComponent<NavMeshAgent>();
        nav.stoppingDistance = defaultStoppingDistance;

        pet.OnDamagedEvent += OnDamaged;
        pet.OnDeathEvent += OnDeath;
    }

    // Functions
    public abstract void Action();

    public Quaternion LookToward(Transform target)
    {
        Vector3 direction = MathUtil.GetDirectionVectorFlat(target.position, pet.Position);
        Quaternion look = Quaternion.LookRotation(direction);
        pet.transform.rotation = Quaternion.Slerp(look, pet.transform.rotation, Time.deltaTime);
        return look;
    }

    public void GoToward(Transform target)
    {
        if (!nav.enabled)
        {
            nav.enabled = true;
        }
        nav.destination = target.position;
    }

    public void GoToward(Vector3 position)
    {
        if (!nav.enabled)
        {
            nav.enabled = true;
        }
        nav.destination = position;
    }

    private void OnDamaged()
    {
        if (nav.enabled)
        {
            nav.velocity = Vector3.zero;
        }
    }

    private void OnDeath()
    {
        GoToward(pet.transform);
        nav.velocity = Vector3.zero;
    }
}