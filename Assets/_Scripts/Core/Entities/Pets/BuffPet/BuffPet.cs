using System.Collections;
using UnityEngine;

public class BuffPet : Companion
{
    // Static attributes
    public const string OBJECT_ID_PREFIX = "BuffPet";

    // Attributes
    protected CombatantEntity ownerDamagingComponent;
    public BuffPetAIController aiController;
    public BuffPetStateController stateController;

    // Constructor
    protected new void Start()
    {
        base.Start();

        SetIdPrefix(OBJECT_ID_PREFIX);
        SetLayer(EnvironmentConfig.LAYER_ENEMY); // Buff pet is from enemy's side

        aiController.Init(this);
        stateController.Init(this);

        IncreaseDamage();
        type = Type.INCREASE;

        aiController.nav.enabled = false;
        transform.position = spawnPosition;
    }

    // Function

    public override void Assign(IAccompaniable owner)
    {
        base.Assign(owner);
        ownerDamagingComponent = Owner.CompanionController.gameObject.GetComponent<BossEntity>(); // BuffPet is a companion of the enemy

#if STRICT
        if (ownerDamagingComponent == null)
        {
            Debug.LogError($"{id}: BuffPet is assigned to a non-BossEntity object");
        }
#endif
    }

    private void IncreaseDamage()
    {
        float prevDamage = ownerDamagingComponent.Damage;
        ownerDamagingComponent?.effects.Add(new StatEffect("Pet buff", StatEffectType.DAMAGE, StatEffectType.MULTIPLICATION, 0.2f, StatEffectFlag.INC_DAMAGE_PET));
        Debug.Log($"{id}: Damage increased from {prevDamage} to {ownerDamagingComponent.Damage}");
    }

    private void DecreaseDamage()
    {
        float prevDamage = ownerDamagingComponent.Damage;
        var index = ownerDamagingComponent.effects.FindIndex(effect => effect.statFlag == StatEffectFlag.INC_DAMAGE_PET);
        if (index >= 0) ownerDamagingComponent.effects.RemoveAt(index);
        Debug.Log($"{id}: Damage decreased from {prevDamage} to {ownerDamagingComponent.Damage}");
    }

    protected new void OnEnable()
    {
        base.OnEnable();
        if (ownerDamagingComponent != null)
        {
            IncreaseDamage();
        }
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        DecreaseDamage();
    }

    // Controller functions
    protected override void FixedUpdateAction()
    {
        aiController?.Action();
        Vector3 dampVelocity = new();
        Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, Vector3.zero, ref dampVelocity, GameConfig.MOVEMENT_SMOOTHING);
    }

    protected override void UpdateAction()
    {
        stateController?.UpdateState();
    }

    // Debugging functions
    protected new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        stateController.VisualizeDetection();
    }

    protected override IEnumerator DeleteBody()
    {

        yield return new WaitForSeconds(2);

        // Remove from companion list
        int index = (Owner as BossEntity).companionList.IndexOf(this);
        if (index == (Owner as BossEntity).CompanionSelectorIndex)
        {
            GameController.Instance.player.CompanionSelectorIndex = 0;
        }

        (Owner as BossEntity).companionList.RemoveAt(index);
        (Owner as BossEntity).companionActive.RemoveAt(index);

        Destroy(gameObject);
    }
}