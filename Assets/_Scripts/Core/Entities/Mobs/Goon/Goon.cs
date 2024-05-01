using System.Collections;
using UnityEngine;

public class Goon : EnemyEntity
{
    // Static Attributes
    public const string ObjectIdPrefix = "Goon";

    // Attributes
    public GoonStateController stateController;
    public GoonAIController aiController;
    public GoonAnimationController animationController;

    // Constructor
    new protected void Start()
    {
        base.Start();
        SetIdPrefix(ObjectIdPrefix);

        stateController = new GoonStateController(this);
        aiController = new GoonAIController(this);
        animationController = new GoonAnimationController(this);

        EquipWeapon(0);
        
        OnDeathEvent += OnDeath;
    }

    // Functions
    protected new void Update()
    {
        base.Update();
        
        stateController.UpdateState();
    }

    protected new void FixedUpdate()
    {
        base.Update();

        aiController.Action();
    }
    
    protected void OnDrawGizmosSelected()
    {
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