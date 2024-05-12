using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;

using UnityEngine;

public class Player : PlayerEntity
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "Player";

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
    public new void Start()
    {
        gameObject.AddComponent<AudioSource>();

        base.Start();
        SetIdPrefix(OBJECT_ID_PREFIX);
        Health *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveManager.Instance.GetActiveGameSave().difficulty].playerHealthMultiplier;
        stats.Init(this);
        stateController.Init(this);
        inputController.Init(this);
        movementController.Init(this);
        animationController.Init(this);
        audioController.Init(this);

        SetLayer(EnvironmentConfig.LAYER_PLAYER);
        SetAttackLayer(EnvironmentConfig.LAYER_PLAYER_ATTACK);
        GameController.Instance.player = this;

        EquipWeapon(0);

        int initialIndex = CompanionList.Count;
        for (int i = 0; i < initialIndex; i++)
        {
            CompanionActive.Add(true);
        }

        ActivateAllCompanions();
    }

    // Functions
    public new void EquipWeapon(int index)
    {
        if (stateController.weaponState != WeaponState.IDLE)
        {
            return;
        }

        base.EquipWeapon(index);
    }

    protected override void UpdateAction()
    {
        inputController?.HandleInputs();
    }

    protected override void FixedUpdateAction()
    {
        movementController?.HandleMovement();
        stateController?.UpdateState();
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

    // Debug Functions
    protected new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        // Visualize stair detection
        Gizmos.color = Color.red;

        if (model != null)
        {
            Vector3 location = model.Bottom;
            location.y += movementController.stairMaxHeight / 2 + movementController.stairDetectionBottomOffset;
            Gizmos.DrawWireCube(location, new(movementController.stairDetectionDistance, movementController.stairMaxHeight, movementController.stairDetectionDistance));
        }

        inputController.VisualizeEnemyRange(this);
    }

    // Orb Functions
    public void ActivateRestoreHealthOrb(float healthMultiplier)
    {
        float prevHealth = Health;
        InflictHeal(healthMultiplier * Health);
        Debug.Log(id + ": Health increased from " + prevHealth + " to " + Health);
    }

    public void ActivateIncSpeedOrb(float duration, StatEffect effect)
    {
        if (isIncSpeedOrbActive)
        {
            Debug.Log(id + ": Increase Speed Orb already active. Killing the previous one.");
            StopCoroutine(incSpeedOrbCoroutine);
        }
        incSpeedOrbCoroutine = StartCoroutine(IncSpeedOrbTimeout(duration, effect));
    }

    private IEnumerator IncSpeedOrbTimeout(float duration, StatEffect effect)
    {
        float prev = Speed;
        if (!isIncSpeedOrbActive)
        {
            effects.Add(effect);
        }

        Debug.Log(id + ": Speed increased from " + prev + " to " + Speed);

        isIncSpeedOrbActive = true;

        yield return new WaitForSeconds(duration);

        effects.Remove(effects.Find(effect => effect.statFlag == StatEffectFlag.INC_SPEED_ORB));
        isIncSpeedOrbActive = false;
        Debug.Log(id + ": Increase Speed Orb effect ended. Speed decreased to " + Speed);
    }

    public void ActivateIncDamageOrb(StatEffect effect)
    {
        incDamageOrbCount++;

        float prev = Damage;
        effects.Add(effect);

        Debug.Log(id + ": Damage increased from " + prev + " to " + Damage);
    }

    public void TeleportAllCompanions(Vector3 destination)
    {
        foreach (var companion in CompanionList)
        {
            Debug.Log($"Warping {companion.name} to {destination}");

            if (companion is AttackingPet)
            {
                AttackingPet comp = companion as AttackingPet;
                comp.aiController.nav.enabled = false;
                comp.transform.position = destination;
                comp.aiController.nav.enabled = true;
            }

            if (companion is HealingPet)
            {
                HealingPet comp = companion as HealingPet;
                comp.aiController.nav.enabled = false;
                comp.transform.position = destination;
                comp.aiController.nav.enabled = true;
            }
        }
    }
}
