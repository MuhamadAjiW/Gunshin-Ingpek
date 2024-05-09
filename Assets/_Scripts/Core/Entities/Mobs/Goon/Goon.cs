using System.Collections;
using UnityEngine;

public class Goon : EnemyEntity
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "Goon";
    public const string AUDIO_CRY_KEY = "cry";

    // Attributes
    public GoonStateController stateController;
    public AudioController audioController;
    public GoonAIController aiController;
    public GoonAnimationController animationController;

    // Constructor
    new protected void Start()
    {
        base.Start();
        SetIdPrefix(OBJECT_ID_PREFIX);

        audioController.Init(this);
        stateController.Init(this);
        aiController.Init(this);
        animationController.Init(this);

        EquipWeapon(0);

        OnDeathEvent += OnDeath;
    }

    // Functions
    protected override void UpdateAction()
    {
        stateController?.UpdateState();
    }

    protected override void FixedUpdateAction()
    {
        animationController?.DetectVisibility();
        aiController?.Action();
        Vector3 dampVelocity = new();
        Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, Vector3.zero, ref dampVelocity, GameConfig.MOVEMENT_SMOOTHING);
    }

    private void OnDeath()
    {
<<<<<<< HEAD
<<<<<<< HEAD
=======
        GameStatisticsManager.Instance.AddGoonsKilled();
>>>>>>> d871ba60 (feat: initial work on statistics element)
=======
        GameStatistics.Instance.AddGoonsKilled();
        GameController.Instance.InvokeEvent(GameConfig.EVENT_ENEMY_KILLED, this);
>>>>>>> 6c55d7c6 (feat: level 2)
        StartCoroutine(DeleteBody());
    }

    private IEnumerator DeleteBody()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Debugging functions    
    protected new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        stateController.VisualizeDetection(this);
        stateController.VisualizePatrolRoute(this);
    }
}