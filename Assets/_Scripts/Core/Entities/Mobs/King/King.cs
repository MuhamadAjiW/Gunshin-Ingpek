using System.Collections;
using UnityEngine;

public class King : BossEntity
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "King";

    // Attributes
    public KingStateController stateController;
    public KingAIController aiController;
    public KingAnimationController animationController;

    // Constructor
    new protected void Start()
    {
        base.Start();
        SetIdPrefix(OBJECT_ID_PREFIX);

        stateController = new KingStateController(this);
        aiController = new KingAIController(this);
        animationController = new KingAnimationController(this);

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