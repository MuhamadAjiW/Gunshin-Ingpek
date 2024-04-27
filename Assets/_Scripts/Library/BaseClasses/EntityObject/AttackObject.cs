using System;
using UnityEngine;

// TODO: Review whether attack object should be classified as a world object
public class AttackObject : MonoBehaviour, IDamaging, IKnockback
{
    // Attributes
    private Vector3 knockbackOffset;
    [SerializeField] private float damage;
    [SerializeField] private float knockbackPower;

    // Events
    public event Action OnDamageEvent;
    
    // Set-Getters
    public float Damage { 
        get => damage; 
        set => damage = value; 
    }
    public float KnockbackPower { 
        get => knockbackPower; 
        set => knockbackPower = value; 
    }
    public Vector3 KnockbackOrigin{
        get => transform.position + knockbackOffset;
        set => knockbackOffset = KnockbackOrigin - transform.position;
    }

    
    // Constructor
    protected void Start()
    {
        if(KnockbackOrigin == null)
        {
            KnockbackOrigin = Vector3.zero;
        } 
    }

    // Functions
    public void Knockback(IRigid rigidObject)
    {
        var knockbackModifier = (-1) * knockbackPower / rigidObject.KnockbackResistance;
        Vector3 knockbackVector = MathUtils.GetDirectionVectorFlat(KnockbackOrigin, rigidObject.Position) * knockbackModifier;
        rigidObject.Rigidbody.AddForce(knockbackVector, ForceMode.Impulse);
    }

    protected bool Hit(Collider otherCollider)
    {    
        // Note: Hitboxes are traditionally placed within a model, therefore we get the damageable component from its parent
        Transform objectParent = otherCollider.transform.parent;
        if(objectParent == null)
        {
            return true;
        }
        objectParent.TryGetComponent<IDamageable>(out var damageableObject);
        if(damageableObject == null)
        {
            return true;
        } 
        
        
        if(damageableObject.Damageable)
        {
            Debug.Log($"Hit in hitbox of {transform.name} by {objectParent.name} with damage of {Damage}");
            
            damageableObject.InflictDamage(Damage);
            OnDamageEvent?.Invoke();

            objectParent.TryGetComponent<IRigid>(out var rigidObject);
            if(rigidObject != null)
            {
                Knockback(rigidObject);
            }

            return true;
        }
        return false;
    }
}
