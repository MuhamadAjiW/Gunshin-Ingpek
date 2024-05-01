using System.Collections;
using UnityEngine;

public class Goon : EnemyEntity
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "Goon";

    // Attributes
    public GoonStateController stateController;
    public GoonAIController aiController;
    public GoonAnimationController animationController;

    // Constructor
    new protected void Start()
    {
        base.Start();
        SetIdPrefix(OBJECT_ID_PREFIX);

        stateController = new GoonStateController(this);
        aiController = new GoonAIController(this);
        animationController = new GoonAnimationController(this);

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