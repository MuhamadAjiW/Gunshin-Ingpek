using UnityEngine;

public abstract class ProjectileObject : AttackObject
{
    // Attributes
    protected float distanceTravelled = 0;
    protected Vector3 position;
    public Vector3 direction;
    public ProjectileData data;

    // Constructor
    protected new void Start()
    {
        base.Start();
        position = transform.position;
    }

    // Functions
    protected void OnTriggerEnter(Collider otherCollider)
    {
        if(Hit(otherCollider) && !data.through)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Move()
    {
        transform.position += direction.normalized * data.speed / 100;
    }

    protected void FixedUpdate()
    {
        Move();
        distanceTravelled += Vector3.Distance(transform.position, position);
        position = transform.position;

        if(distanceTravelled >= data.travelDistance)
        {
            Destroy(gameObject);
        }
    }
}