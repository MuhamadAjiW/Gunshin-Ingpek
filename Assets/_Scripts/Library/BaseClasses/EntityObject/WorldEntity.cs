using System;
using UnityEngine;

public class WorldEntity : WorldObject, IRigid
{
    // Attributes
    [NonSerialized] public Model model;
    [SerializeField] private float knockbackResistance;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] protected LayerMask groundLayers;
    protected Vector3 groundDetectionSize; 
    protected new Rigidbody rigidbody;
    private bool grounded = false;

    // Events
    public event Action OnGroundedEvent;

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
    protected new void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        model = GetComponentInChildren<Model>();
        
        #if STRICT
        if(rigidbody == null)
        {
            Debug.LogError($"Rigid entity {name} does not have a rigidbody. How to resolve: Add a rigidbody component to it"); 
        }
        if(model == null) 
        {
            Debug.LogError($"Rigid entity {name} does not have a model. How to resolve: Create a gameObject with a model.cs script as its child");
        }
        #endif

        groundDetectionSize = new Vector3(0.3f, 0.3f, 0.3f);      
        groundLayers = LayerMask.GetMask(EnvironmentConfig.LAYER_DEFAULT);
        model.gameObject.layer = LayerMask.NameToLayer(LayerCode);
    }

    // Functions
    public new void SetLayer(string layerCode)
    {
        base.SetLayer(layerCode);
        model.gameObject.layer = LayerMask.NameToLayer(LayerCode);
    }

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
        if(GameController.Instance.IsPaused)
        {
            return;
        }
    }

    protected void FixedUpdate()
    {
        if(GameController.Instance.IsPaused)
        {
            return;
        }

        Collider[] groundOverlaps = Physics.OverlapBox(model.Bottom, groundDetectionSize, Quaternion.identity, groundLayers);
        if(!grounded && groundOverlaps.Length != 0)
        {
            OnGroundedEvent?.Invoke();
        }
        grounded = groundOverlaps.Length != 0;
    }
}