using System.Collections;
using UnityEngine;

public class King : BossEntity
{
    // Static Attributes
    public const string GOON_PREFAB = "Prefabs/Mobs/Goon/Goon";
    public const string GOON_RIFLE_PREFAB = "Prefabs/Mobs/Goon/Goon_Rifle";
    public const string OBJECT_ID_PREFIX = "King";

    // Attributes
    public int goonCountLimit = 5;
    public int drainDamage = 1;
    public int drainDelay = 5;
    public KingStateController stateController;
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

        stateController.Init(this);
        aiController.Init(this);
        animationController.Init(this);

        EquipWeapon(0);
        
        OnDeathEvent += OnDeath;
        StartCoroutine(SpawnGoons());
        StartCoroutine(DrainPlayerHealth());

        stateController.OnPlayerEnterDebuffEffect += RegisterPlayerDebuff;
        stateController.OnPlayerLeaveDebuffEffect += UnregisterPlayerDebuff;
    }

    // Functions
    protected void RegisterPlayerDebuff()
    {
        GameController.Instance.player.effects.Add(damageDebuff);
        GameController.Instance.player.effects.Add(speedDebuff);
    }
    
    protected void UnregisterPlayerDebuff()
    {
        GameController.Instance.player.effects.Remove(damageDebuff);
        GameController.Instance.player.effects.Remove(speedDebuff);
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

    protected IEnumerator SpawnGoons()
    {
        if(goonCount < goonCountLimit && !Dead)
        {
            int rng = UnityEngine.Random.Range(0, 2);
            
            Goon goon;
            if(rng == 0)
            {
                goon = ObjectFactory.CreateEntity<Goon>(
                    prefabPath: GOON_RIFLE_PREFAB,
                    position: transform.position + transform.up
                );
            }
            else{
                goon = ObjectFactory.CreateEntity<Goon>(
                    prefabPath: GOON_PREFAB,
                    position: transform.position + transform.up
                );
            }
            goon.stateController.detectionDistance = stateController.detectionDistance + 1;
            goon.OnDeathEvent += OnGoonDeath;
            goonCount++;
        }
        yield return new WaitForSeconds(15);
        if(!Dead)
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
    
    protected new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        stateController.VisualizeDetection(this);
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