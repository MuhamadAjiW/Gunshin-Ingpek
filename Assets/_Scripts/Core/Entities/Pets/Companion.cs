using System;
using System.Collections;
using UnityEngine;

public abstract class Companion : DamageableEntity
{

    public enum Type
    {
        HEALING,
        DAMAGE,
        INCREASE
    }

    // Attributes
    public CompanionData data;

    public Type type;


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
