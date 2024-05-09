using System.Collections;
using UnityEngine;

public class General : BossEntity
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "General";
    public const string AUDIO_CRY_KEY = "cry";
    public const string AUDIO_DIE_KEY = "die";
    public const string AUDIO_ATTACK_KEY = "attack";

    // Attributes
    public float drainDamage = 1;
    public float drainDelay = 5;
    public GeneralStateController stateController;
    public AudioController audioController;
    public GeneralAIController aiController;
    public GeneralAnimationController animationController;

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
<<<<<<< HEAD
        ActivateAllCompanions();
=======
>>>>>>> d871ba60 (feat: initial work on statistics element)

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
        if (stateController.playerInDebuff && !GameController.Instance.player.Dead)
        {
            GameController.Instance.player.InflictDrainDamage(drainDamage);
        }

        if (!Dead)
        {
            StartCoroutine(DrainPlayerHealth());
        }
    }

    private void OnDeath()
    {
<<<<<<< HEAD
=======
        audioController.Play(AUDIO_DIE_KEY);
<<<<<<< HEAD
        GameStatistics.Instance.AddGeneralsKilled();
<<<<<<< HEAD
>>>>>>> cc490e85 (feat: mob sounds)
=======
        GameStatisticsManager.Instance.AddGeneralsKilled();
>>>>>>> d871ba60 (feat: initial work on statistics element)
=======
        GameController.Instance.InvokeEvent(GameConfig.EVENT_ENEMY_KILLED, this);
>>>>>>> 6c55d7c6 (feat: level 2)
        StartCoroutine(DeleteBody());
    }

    private IEnumerator DeleteBody()
    {
        yield return new WaitForSeconds(2);
        // foreach (Companion companion in CompanionList)
        // {
        //     Destroy(companion.gameObject);
        // }
        Destroy(gameObject);
    }

    // Debugging functions
    protected new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        stateController.VisualizeDetection(this);
        stateController.VisualizePatrolRoute(this);
    }
    protected void OnBecameInvisible()
    {
        animationController.OnBecameInvisible();
    }
    
    protected void OnBecameVisible()
    {
        animationController.OnBecameVisible();
    }
}