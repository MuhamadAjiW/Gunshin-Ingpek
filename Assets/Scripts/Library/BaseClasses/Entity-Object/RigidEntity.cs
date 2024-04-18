using UnityEngine;

public class RigidEntity : MonoBehaviour, IRigid {
    // Attributes
    [SerializeField] private float knockbackResistance;

    // Readonly by others
    private new Rigidbody rigidbody;
    private new Collider collider;

    //TODO: grounded automatic detection
    private bool grounded = true;


    public Rigidbody Rigidbody => rigidbody;
    public Collider Collider => collider;
    public Vector3 Position => transform.position;
    public bool Grounded => grounded;
    public float KnockbackResistance {
        get => knockbackResistance <= 0? 1 : knockbackResistance;
        set => knockbackResistance = value;
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