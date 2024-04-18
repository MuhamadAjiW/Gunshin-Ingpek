using UnityEngine;

public class RigidEntity : MonoBehaviour, IRigid {
    // Attributes
    [SerializeField] private float knockbackResistance;
    [SerializeField] private float baseSpeed;
    private new Rigidbody rigidbody;
    private new Collider collider;
    
    public Rigidbody Rigidbody => rigidbody;
    public Collider Collider => collider;
    public Vector3 Position => transform.position;
    public float KnockbackResistance {
        get => knockbackResistance <= 0? 1 : knockbackResistance;
        set => knockbackResistance = value;
    }
    public float BaseSpeed { 
        get => baseSpeed; 
        set => baseSpeed = value < 0? 0 : value; 
    }

    // Constructor

    protected void Start(){
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    // Functions
    protected void Refresh(){
        Rigidbody.AddForce(Vector2.zero);
    }

    protected void Smoothen(){
        Vector3 dampVelocity = Vector3.zero;
        Vector3 velocity = Rigidbody.velocity;
        velocity.x = 0;
        velocity.z = 0;
        Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, velocity, ref dampVelocity, GameConfig.MOVEMENT_SMOOTHING);
    }

    protected void Update(){
        if(GameController.instance.IsPaused) return;
    }

    protected void FixedUpdate(){
        if(GameController.instance.IsPaused) return;
    }
}