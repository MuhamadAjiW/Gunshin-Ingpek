using UnityEngine;

public abstract class ProjectileObject : AttackObject{
    // Attributes
    protected float distanceTravelled = 0;
    protected Vector3 position;
    public Vector3 direction;
    public float speed;
    public float travelDistance;
    public bool through;

    // Constructor
    protected new void Start(){
        base.Start();
        position = transform.position;
    }

    // Functions
    protected void OnTriggerEnter(Collider otherCollider){
        if(Hit(otherCollider) && !through) Destroy(gameObject);
    }

    protected virtual void Move(){
        transform.position += direction.normalized * speed / 100;
    }

    protected void FixedUpdate(){
        Move();
        distanceTravelled += Vector3.Distance(transform.position, position);
        position = transform.position;

        if(distanceTravelled >= travelDistance) Destroy(gameObject);
    }
}