using System;
using UnityEngine;

// TODO: Review whether attack object should be classified as a world object
public class AttackObject : MonoBehaviour, IAttack{
    // Attributes
    private Vector3 knockbackOffset;
    [SerializeField] private float damage;
    [SerializeField] private float knockbackPower;
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
    protected void Start(){
        if(KnockbackOrigin == null) KnockbackOrigin = Vector3.zero;
    }

    // Functions
    public void Knockback(IRigid rigidObject){
        var knockbackModifier = (-1) * knockbackPower / rigidObject.KnockbackResistance;
        Vector3 knockbackVector = MathUtils.GetDirectionVectorFlat(KnockbackOrigin, rigidObject.Position) * knockbackModifier;
        rigidObject.Rigidbody.AddForce(knockbackVector, ForceMode.Impulse);
    }

    protected bool Hit(Collider otherCollider){
        otherCollider.transform.TryGetComponent<IDamageable>(out var damageableObject);
        if(damageableObject == null) return true;
        
        Debug.Log(string.Format("Hit in hitbox of {0} by {1} with damage of {2}", transform.name, otherCollider.transform.name, Damage));
        
        if(damageableObject.Damageable){
            
            damageableObject.InflictDamage(Damage);
            OnDamageEvent?.Invoke();

            otherCollider.TryGetComponent<IRigid>(out var rigidObject);
            if(rigidObject != null) Knockback(rigidObject);

            return true;
        }
        return false;
    }
}
