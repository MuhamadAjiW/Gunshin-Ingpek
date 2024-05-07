using System.Collections;
using UnityEngine;

public class King : BossEntity
{
    // Static Attributes
    public const string GOON_PREFAB = "Prefabs/Mobs/Goon/Goon";
    public const string GOON_RIFLE_PREFAB = "Prefabs/Mobs/Goon/Goon_Rifle";
    public const string OBJECT_ID_PREFIX = "King";
    public const string AUDIO_CRY_KEY = "cry";
    public const string AUDIO_DIE_KEY = "die";
    public const string AUDIO_ATTACK_KEY = "attack";

    // Attributes
    public int goonCountLimit = 5;
    public float drainDamage = 1;
    public float drainDelay = 5;
    public KingStateController stateController;
    public AudioController audioController;
    public KingAIController aiController;
    public KingAnimationController animationController;
    protected int goonCount = 0;
    private readonly StatEffect speedDebuff = new("King_Debuff", StatEffectType.SPEED, StatEffectType.MULTIPLICATION, -0.15f);
    private readonly StatEffect damageDebuff = new("King_Debuff", StatEffectType.DAMAGE, StatEffectType.MULTIPLICATION, -0.15f);

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
        StartCoroutine(SpawnGoons());
        StartCoroutine(DrainPlayerHealth());

        stateController.OnPlayerEnterDebuffEvent += RegisterPlayerDebuff;
        stateController.OnPlayerLeaveDebuffEvent += UnregisterPlayerDebuff;
    }

    // Functions
    protected void RegisterPlayerDebuff()
    {
        if(GameController.Instance.player.Dead)
        {
            return;
        }
        GameController.Instance.player.effects.Add(damageDebuff);
        GameController.Instance.player.effects.Add(speedDebuff);
    }

    protected void UnregisterPlayerDebuff()
    {
        if(GameController.Instance.player.Dead)
        {
            return;
        }
        GameController.Instance.player.effects.Remove(damageDebuff);
        GameController.Instance.player.effects.Remove(speedDebuff);
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

    protected IEnumerator SpawnGoons()
    {
        if (goonCount < goonCountLimit && !Dead)
        {
            int rng = UnityEngine.Random.Range(0, 2);

            Goon goon;
            if (rng == 0)
            {
                goon = ObjectFactory.CreateEntity<Goon>(
                    prefabPath: GOON_RIFLE_PREFAB,
                    position: transform.position + transform.up,
                    objectName: $"{name}'s Goons"
                );
            }
            else
            {
                goon = ObjectFactory.CreateEntity<Goon>(
                    prefabPath: GOON_PREFAB,
                    position: transform.position + transform.up,
                    objectName: $"{name}'s Goons"
                );
            }
            goon.stateController.detectionDistance = stateController.detectionDistance;
            goon.OnDeathEvent += OnGoonDeath;
            goonCount++;
        }
        yield return new WaitForSeconds(15);
        if (!Dead)
        {
            StartCoroutine(SpawnGoons());
        }
    }

    protected void OnGoonDeath()
    {
        goonCount--;
    }

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

    private void OnDeath()
    {
<<<<<<< HEAD
=======
        audioController.Play(AUDIO_DIE_KEY);
<<<<<<< HEAD
        GameStatistics.Instance.AddKingsKilled();
>>>>>>> cc490e85 (feat: mob sounds)
=======
        GameStatisticsManager.Instance.AddKingsKilled();
>>>>>>> d871ba60 (feat: initial work on statistics element)
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
}