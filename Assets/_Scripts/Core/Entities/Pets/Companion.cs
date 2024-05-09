using System;
<<<<<<< HEAD
using System.Collections;
=======
using Unity.VisualScripting;
>>>>>>> 42daf667 (feat: added companion aggregation)
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

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
<<<<<<< HEAD
            Type.HEALING => new HealingCompanion(),
            Type.DAMAGE => new HealingCompanion(),
            Type.INCREASE => new HealingCompanion(),
            _ => new(),
=======
            Type.HEALING => EventManager.Instance.CompanionPool[1],
            Type.DAMAGE => EventManager.Instance.CompanionPool[0],
            _ => null,
>>>>>>> 84fdb8a2 (fix: pet init)
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

    private IEnumerator DeleteBody()
    {
        yield return new WaitForSeconds(2);

        // Remove from companion list
        int index = GameController.Instance.player.companionList.IndexOf(this);
        if (index == GameController.Instance.player.CompanionSelectorIndex)
        {
            GameController.Instance.player.CompanionSelectorIndex = 0;
        }

        GameController.Instance.player.companionList.RemoveAt(index);
        GameController.Instance.player.companionActive.RemoveAt(index);

        Destroy(gameObject);
    }

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
