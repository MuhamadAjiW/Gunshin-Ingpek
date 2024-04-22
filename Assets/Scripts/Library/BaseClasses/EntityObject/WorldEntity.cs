using UnityEngine;

public class WorldEntity : WorldObject, IRigid
{
    // Attributes
    [SerializeField] private float knockbackResistance;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] protected LayerMask groundLayers;
    protected Vector3 groundDetectionSize; 
    protected new Rigidbody rigidbody;
    private bool grounded = false;

    // Set-Getters
    public Rigidbody Rigidbody => rigidbody;
    public Vector3 Position => transform.position;
    public bool Grounded => grounded;
    public float KnockbackResistance
    {
        get => knockbackResistance <= 0? 1 : knockbackResistance;
        set => knockbackResistance = value;
    }
    public float BaseSpeed 
    { 
        get => baseSpeed; 
        set => baseSpeed = value < 0? 0 : value; 
    }
    public float JumpForce 
    { 
        get => jumpForce; 
        set => jumpForce = value < 0? 0 : value; 
    }

    // Constructor
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        groundDetectionSize = new Vector3(0.05f, 0.05f, 0.05f);      
        if(rigidbody == null)
        {
            Debug.LogWarning($"Rigid entity {name} does not have a rigidbody"); 
        }
        groundLayers = LayerMask.GetMask(EnvironmentConfig.LAYER_DEFAULT);
    }

    // Functions
    protected void Refresh()
    {
        Rigidbody.AddForce(Vector2.zero);
    }

    protected void Smoothen()
    {
        Vector3 dampVelocity = Vector3.zero;
        Vector3 velocity = Rigidbody.velocity;
        velocity.x = 0;
        velocity.z = 0;
        Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, velocity, ref dampVelocity, GameConfig.MOVEMENT_SMOOTHING);
    }

    protected void Update()
    {
        if(GameController.instance.IsPaused)
        {
            return;
        }
    }

    protected void FixedUpdate()
    {
        if(GameController.instance.IsPaused)
        {
            return;
        }

        Vector3 center = transform.position;
        Collider[] groundOverlaps = Physics.OverlapBox(center, groundDetectionSize, Quaternion.identity, groundLayers);
        grounded = groundOverlaps.Length != 0;
    }
}