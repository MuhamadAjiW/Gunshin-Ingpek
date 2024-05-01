using System.Collections;
using UnityEngine;

public class General : BossEntity
{
    // Static Attributes
    public const string ObjectIdPrefix = "General";

    // Attributes
    public GeneralStateController stateController;
    public GeneralAIController aiController;
    public GeneralAnimationController animationController;

    // Constructor
    new protected void Start()
    {
        base.Start();
        SetIdPrefix(ObjectIdPrefix);

        stateController = new GeneralStateController(this);
        aiController = new GeneralAIController(this);
        animationController = new GeneralAnimationController(this);

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