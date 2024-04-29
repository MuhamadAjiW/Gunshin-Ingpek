using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class Player : PlayerEntity
{
    // Static Attributes
    public const string ObjectIdPrefix = "Player";

    // Attributes
    public PlayerMovementController movementController;
    public PlayerAnimationController animationController;
    public PlayerInputController inputController;
    public PlayerStateController stateController;
    public PlayerAudioController audioController;
    public PlayerStats stats;

    // Orb Attributes
    private Coroutine incSpeedOrbCoroutine;
    private bool isIncSpeedOrbActive = false;
    [NonSerialized] public int incDamageOrbCount = 0;
    public int maxIncDamageOrbCount = 15;

    // Constructor
    new void Start()
    {
        gameObject.AddComponent<AudioSource>();

        base.Start();
        SetIdPrefix(ObjectIdPrefix);
        Health *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.Instance.difficulty].playerHealthMultiplier;
        stats = new PlayerStats(this);
        stateController = new PlayerStateController(this);
        inputController = new PlayerInputController(this);
        movementController = new PlayerMovementController(this);
        animationController = new PlayerAnimationController(this);
        audioController = new PlayerAudioController(this, audioController.audios);

        SetLayer(EnvironmentConfig.LAYER_PLAYER);
        SetAttackLayer(EnvironmentConfig.LAYER_PLAYER_ATTACK);
        GameController.Instance.player = this;


        // TODO: These are for dev, consider deleting
        WeaponList.AddRange(GetComponentsInChildren<WeaponObject>());
        EquipWeapon(0);
        // ----------/TODO

        int initialIndex = CompanionList.Count;
        for (int i = 0; i < initialIndex; i++)
        {
            CompanionActive.Add(false);
        }

        CompanionList.AddRange(EntityManager.Instance.GetComponentsInChildren<Companion>());
        for (int i = initialIndex; i < CompanionList.Count; i++)
        {
            CompanionActive.Add(CompanionList[i].gameObject.activeSelf);
        }
    }

    // Functions
    public new void EquipWeapon(int index)
    {
        if (stateController.weaponState != WeaponState.IDLE)
        {
            return;
        }

        base.EquipWeapon(index);
        Weapon.transform.localScale = new(0.01f, 0.01f, 0.01f);
    }



    protected new void Update()
    {
        base.Update();
        if (Dead || GameController.Instance.IsPaused)
        {
            return;
        }

        inputController.HandleInputs();
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
        if (Dead || GameController.Instance.IsPaused)
        {
            return;
        }

        movementController.HandleMovement();
        stateController.UpdateState();
    }

    protected void OnTriggerEnter(Collider otherCollider)
    {
        otherCollider.transform.TryGetComponent<IInteractable>(out var interactable);

        if (interactable == null)
        {
            return;
        }

        interactable.InvokeOnInteractAreaEnterEvent();
        stateController.currentInteractables.Add(interactable);
    }

    protected void OnTriggerExit(Collider otherCollider)
    {
        otherCollider.transform.TryGetComponent<IInteractable>(out var interactable);

        if (interactable == null)
        {
            return;
        }

        interactable.InvokeOnInteractAreaExitEvent();
        stateController.currentInteractables.Remove(interactable);
    }

    protected void OnDrawGizmosSelected()
    {
        // Visualize stair detection
        Gizmos.color = Color.red;

        if (model != null)
        {
            Vector3 location = model.Bottom;
            location.y += movementController.stairMaxHeight / 2 + movementController.stairDetectionBottomOffset;
            Gizmos.DrawWireCube(location, new(movementController.stairDetectionDistance, movementController.stairMaxHeight, movementController.stairDetectionDistance));
        }
    }

    // Orb Functions
    public void ActivateRestoreHealthOrb(float healthMultiplier)
    {
        float prevHealth = Health;
        InflictHeal(healthMultiplier * Health);
        Debug.Log(id + ": Health increased from " + prevHealth + " to " + Health);
    }

    public void ActivateIncSpeedOrb(float duration, float speedMultiplier)
    {
        if (isIncSpeedOrbActive)
        {
            Debug.Log(id + ": Increase Speed Orb already active. Killing the previous one.");
            StopCoroutine(incSpeedOrbCoroutine);
        }
        incSpeedOrbCoroutine = StartCoroutine(IncSpeedOrbTimeout(duration, speedMultiplier));
    }

    private IEnumerator IncSpeedOrbTimeout(float duration, float speedMultiplier)
    {
        float prevBaseSpeed = BaseSpeed;
        float actualMultiplier = 1 + speedMultiplier;
        if (!isIncSpeedOrbActive)
        {
            BaseSpeed *= actualMultiplier;
        }

        Debug.Log(id + ": Base Speed increased from " + prevBaseSpeed + " to " + BaseSpeed);

        isIncSpeedOrbActive = true;

        yield return new WaitForSeconds(duration);

        BaseSpeed /= actualMultiplier;
        isIncSpeedOrbActive = false;
        Debug.Log(id + ": Increase Speed Orb effect ended. Base Speed decreased to " + BaseSpeed);
    }

    public void ActivateIncDamageOrb(float baseDamageMultiplier)
    {
        incDamageOrbCount++;
        float prevBaseDamage = BaseDamage;

        if (incDamageOrbCount == 1)
        {
            BaseDamage *= 1 + baseDamageMultiplier;
        }
        else
        {
            BaseDamage = BaseDamage / (1 + (incDamageOrbCount - 1) * baseDamageMultiplier) * (1 + incDamageOrbCount * baseDamageMultiplier);
        }

        Debug.Log(id + ": Base Damage increased from " + prevBaseDamage + " to " + BaseDamage);
    }
}
