using System.Collections;
using UnityEngine;

public class AttackingPet : Companion, IArmed
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "AttackingPet";

    // Attribute
    public AttackingPetAIController aiController;
    public AttackingPetStateController stateController;

    protected float baseDamage = 5;

    public WeaponObject weaponSlot;
    private WeaponObject weapon;
    private float attackMultiplier = 1;
    private string attackLayerCode = EnvironmentConfig.LAYER_PLAYER_ATTACK;

    public WeaponObject WeaponSlot => weaponSlot;
    public WeaponObject Weapon => weapon;
    public Transform Orientation => transform;
    public Vector3 WeaponLocation => transform.position;

    public float AttackMultiplier
    {
        get => attackMultiplier;
        set => attackMultiplier = value;
    }

    public string AttackLayerCode
    {
        get => attackLayerCode;
        set => attackLayerCode = value;
    }
    public float Damage
    {
        get => baseDamage;
        set => baseDamage = value;
    }

    protected new void Start()
    {
        base.Start();

        SetIdPrefix(OBJECT_ID_PREFIX);
        SetLayer(EnvironmentConfig.LAYER_PLAYER); // Attacking pet is from player's side
        EquipWeapon();

        aiController.Init(this);
        aiController.nav.enabled = false;
        transform.position = spawnPosition;

        stateController.Init(this);
        type = Type.DAMAGE;
    }

    private void EquipWeapon()
    {
#if STRICT
        if (WeaponSlot == null)
        {
            Debug.LogError($"{id}: AttackingPet {name} does not have any weapon.");
        }
#endif

        WeaponObject selectedWeapon = WeaponSlot;
        selectedWeapon = ObjectFactory.CreateObject<WeaponObject>(
                    prefabPath: selectedWeapon.data.prefabPath,
                    parent: gameObject.transform,
                    objectName: selectedWeapon.name
                );
        weaponSlot = selectedWeapon;
        selectedWeapon.gameObject.SetActive(true);
        selectedWeapon.gameObject.layer = LayerMask.NameToLayer(AttackLayerCode);
        weapon = selectedWeapon;
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
        stateController.VisualizeDetection(this);
    }

    protected override IEnumerator DeleteBody()
    {

        yield return new WaitForSeconds(2);

        // Remove from companion list
        int index = GameController.Instance.player.companionList.IndexOf(this);
        if (index == GameController.Instance.player.CompanionSelectorIndex)
        {
            GameController.Instance.player.CompanionSelectorIndex = 0;
        }

        GameController.Instance.player.companionList.RemoveAt(index);
        GameController.Instance.player.companionActive.RemoveAt(index);

        Destroy(gameObject);

    }
}