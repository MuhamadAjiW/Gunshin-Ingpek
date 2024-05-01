using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGoon : EnemyEntity
{
    // Static Attributes
    public const string GOON_PREFAB = "Prefabs/Mobs/Goon/Goon";
    public const string OBJECT_ID_PREFIX = "HeadGoon";

    // Attributes
    protected int goonCount = 0;
    public int goonCountLimit = 2;
    public HeadGoonStateController stateController;
    public HeadGoonAIController aiController;
    public HeadGoonAnimationController animationController;

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
    }

    // Functions
    protected IEnumerator SpawnGoons()
    {
        if(goonCount < goonCountLimit && !Dead)
        {            
            Goon goon = ObjectFactory.CreateEntity<Goon>(
                prefabPath: GOON_PREFAB,
                position: transform.position + transform.up
            );
            goon.OnDeathEvent += OnGoonDeath;
            goonCount++;
        }
        yield return new WaitForSeconds(25);
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