using System;
using UnityEngine;

public class AttackObject : MonoBehaviour, IAttack{
    // Attributes
    [SerializeField] private float damage;
    [SerializeField] private float knockbackPower;
    public float Damage { 
        get => damage; 
        set => damage = value; 
    }
    public float KnockbackPower { 
        get => knockbackPower; 
        set => knockbackPower = value; 
    }
    public Vector3 KnockbackOrigin{get; set;}
    public event Action OnDamageEvent;

    // Functions
    public void Knockback(IRigid rigidObject){
        var knockbackModifier = (-1) * knockbackPower / rigidObject.KnockbackResistance;
        Vector3 knockbackVector = MathUtils.GetDirectionVector(KnockbackOrigin, rigidObject.Position) * knockbackModifier;
        rigidObject.Rigidbody.AddForce(knockbackVector, ForceMode.Impulse);
        Debug.Log(knockbackVector);
    }

    protected void Hit(Collider otherCollider){

        otherCollider.transform.TryGetComponent<IDamageable>(out var damageableObject);
        if(damageableObject == null) return;
        
        if(damageableObject.Damageable){
            Debug.Log(string.Format("Hit in hitbox of {0} by {1} with damage of {2}", transform.name, otherCollider.transform.name, Damage));
            
            damageableObject.InflictDamage(Damage);
            OnDamageEvent?.Invoke();

            otherCollider.TryGetComponent<IRigid>(out var rigidObject);
            if(rigidObject != null) Knockback(rigidObject);
        }
    }
}
