using UnityEngine;

public class WorldEntity : WorldObject, IRigid
{
    // Attributes
    [SerializeField] private float knockbackResistance;
    [SerializeField] private float baseSpeed;
    private new Rigidbody rigidbody;
    
    // Set-Getters
    public Rigidbody Rigidbody => rigidbody;
    public Vector3 Position => transform.position;
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

    // Constructor

    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();        
        if(rigidbody == null)
        {
            Debug.LogWarning("Rigid entity " + name + " does not have a rigidbody"); 
        }
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
    }
}