using System.Collections;
using UnityEngine;

public class King : BossEntity
{
    // Static Attributes
    public const string GOON_PREFAB = "Prefabs/Mobs/Goon/Goon";
    public const string GOON_RIFLE_PREFAB = "Prefabs/Mobs/Goon/Goon_Rifle";
    public const string OBJECT_ID_PREFIX = "King";

    // Attributes
    protected int goonCount = 0;
    public int goonCountLimit = 5;
    public KingStateController stateController;
    public KingAIController aiController;
    public KingAnimationController animationController;

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
    }

    // Functions
    protected IEnumerator DrainPlayerHealth()
    {
        yield return new WaitForSeconds(5);
        if (stateController.playerInDebuff)
        {
            GameController.Instance.player.InflictDrainDamage(1);
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