using System.Collections;
using UnityEngine;

public class HeadGoon : EnemyEntity
{
    // Static Attributes
    public const string ObjectIdPrefix = "HeadGoon";

    // Attributes
    public HeadGoonStateController stateController;
    public HeadGoonAIController aiController;
    public HeadGoonAnimationController animationController;

    // Constructor
    new protected void Start()
    {
        base.Start();
        SetIdPrefix(ObjectIdPrefix);

        stateController = new HeadGoonStateController(this);
        aiController = new HeadGoonAIController(this);
        animationController = new HeadGoonAnimationController(this);

        EquipWeapon(0);
        
        OnDeathEvent += OnDeath;
    }

    // Functions
    protected override void UpdateAction()
    {
        stateController.UpdateState();
    }

    protected override void FixedUpdateAction()
    {
        aiController.Action();
    }
    
    protected new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        stateController.VisualizeGizmos();
    }

    private void OnDeath()
    {
        StartCoroutine(DeleteBody());
    }

    private IEnumerator DeleteBody()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}