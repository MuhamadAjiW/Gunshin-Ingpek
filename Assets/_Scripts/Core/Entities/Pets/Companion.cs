using System.Collections;
using UnityEngine;

public abstract class Companion : DamageableEntity
{

    public int poolIndex;
    public enum Type
    {
        HEALING,
        DAMAGE,
        INCREASE
    }

    // Attributes
    public CompanionData data;

    public Type type;
    public Vector3 spawnPosition;

    public static string GetCompanionTypeNameFromEnum(Type type)
    {
        return type switch
        {
            Type.HEALING => "Healer",
            Type.DAMAGE => "Damage dealer",
            Type.INCREASE => "Increaser",
            _ => "Not valid companion!",
        };
    }

    public static Companion NewCompanionByType(Type type)
    {
        return type switch
        {
            Type.HEALING => EventManager.Instance.CompanionPool[1],
            Type.DAMAGE => EventManager.Instance.CompanionPool[0],
            _ => null,
        };
    }

    public string TypeName
    {
        get => GetCompanionTypeNameFromEnum(type);
    }


    // Getter-Setter
    public IAccompaniable Owner { get; set; }

    // Constructor
    public virtual void Assign(IAccompaniable owner)
    {
        Owner = owner;
    }

    protected abstract IEnumerator DeleteBody();

    private void OnDeath()
    {
        Debug.Log($"{id}: Die");
        StartCoroutine(DeleteBody());
    }

    new protected void Start()
    {
        base.Start();
        OnDeathEvent += OnDeath;
    }

    protected void OnEnable()
    {
        Debug.Log($"{id}: Enabled");
    }

    protected void OnDisable()
    {
        Debug.Log($"{id}: Disabled");
    }
}
