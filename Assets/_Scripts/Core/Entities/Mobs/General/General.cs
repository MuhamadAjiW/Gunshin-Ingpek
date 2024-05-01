using System.Collections;
using UnityEngine;

public class General : BossEntity
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "General";

    // Attributes
    public int drainDamage = 1;
    public int drainDelay = 5;
    public GeneralStateController stateController;
    public GeneralAIController aiController;
    public GeneralAnimationController animationController;

    // Constructor
    new protected void Start()
    {
        base.Start();
        SetIdPrefix(OBJECT_ID_PREFIX);

        stateController.Init(this);
        aiController.Init(this);
        animationController.Init(this);

        EquipWeapon(0);
        
        OnDeathEvent += OnDeath;
        StartCoroutine(DrainPlayerHealth());
    }

    // Functions
    protected override void UpdateAction()
    {
        stateController?.UpdateState();
    }

    protected override void FixedUpdateAction()
    {
        aiController?.Action();

        Vector3 dampVelocity = new();
        Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, Vector3.zero, ref dampVelocity, GameConfig.MOVEMENT_SMOOTHING);
    }
    
    protected IEnumerator DrainPlayerHealth()
    {
        yield return new WaitForSeconds(drainDelay);
        if (stateController.playerInDebuff)
        {
            GameController.Instance.player.InflictDrainDamage(drainDamage);
        }

        if(!Dead)
        {
            StartCoroutine(DrainPlayerHealth());
        }
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

    // Debugging purposes
    protected new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        stateController.VisualizeDetection(this);
    }
}