using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGoon : EnemyEntity
{
    // Static Attributes
    public const string GOON_PREFAB = "Prefabs/Mobs/Goon/Goon";
    public const string OBJECT_ID_PREFIX = "HeadGoon";
    public const string AUDIO_CRY_KEY = "cry";

    // Attributes
    protected int goonCount = 0;
    public int goonCountLimit = 2;
    public HeadGoonStateController stateController;
    public AudioController audioController;
    public HeadGoonAIController aiController;
    public HeadGoonAnimationController animationController;

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
        StartCoroutine(SpawnGoons());
    }

    // Functions
    protected IEnumerator SpawnGoons()
    {
        if (goonCount < goonCountLimit && !Dead)
        {
            Goon goon = ObjectFactory.CreateEntity<Goon>(
                prefabPath: GOON_PREFAB,
                position: transform.position + transform.up,
                objectName: $"{name}'s Goons"
            );
            goon.OnDeathEvent += OnGoonDeath;
            goon.stateController.detectionDistance = stateController.detectionDistance;
            goon.aiController.patrolRoute = aiController.patrolRoute;
            goonCount++;
        }
        yield return new WaitForSeconds(25);
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
<<<<<<< HEAD
=======
        GameStatisticsManager.Instance.AddHeadGoonsKilled();
>>>>>>> d871ba60 (feat: initial work on statistics element)
=======
        GameStatistics.Instance.AddHeadGoonsKilled();
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