using System;
using System.Collections;
using _Scripts.Core.Game.Data;
using UnityEngine;

[Serializable]
public class PlayerInputController
{
    // Attributes
    private Player player;
    [HideInInspector] public float movementInputX;
    [HideInInspector] public float movementInputZ;
    [HideInInspector] public float movementInputScroll;
    [HideInInspector] public bool movementInputJump;
    protected float attackWindowSize = 0.3f;
    protected Coroutine attackWindowCoroutine;
    protected LayerMask enemyLayer;
    public float enemyCloseRange = 1f;

    // Events
    public event Action OnJumpEvent;
    public event Action<bool> OnAimEvent;


    // Constructor
    public void Init(Player player)
    {
        this.player = player;
        enemyLayer = LayerMask.GetMask(EnvironmentConfig.LAYER_ENEMY);
    }

    // Functions
    public void HandleInputs()
    {
        movementInputX = Input.GetAxisRaw("Horizontal");
        movementInputZ = Input.GetAxisRaw("Vertical");
        movementInputScroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if (Input.GetButtonDown("Jump") && player.Grounded)
        {
            OnJumpEvent?.Invoke();
        }

        if (Input.GetKeyDown(GameInput.Instance.aimToggleButton))
        {
            player.stateController.ToggleIsAiming();
            OnAimEvent?.Invoke(player.stateController.GetIsAiming());
        }

        bool Toggled = Input.GetKey(GameInput.Instance.inputToggleButton);
        if (Toggled)
        {
            HandleToggledInputs();
        }
        else
        {
            HandleUntoggledInputs();
        }
    }

    private void HandleUntoggledInputs()
    {
        if (movementInputScroll != 0)
        {
            player.EquipWeapon(player.WeaponIndex + (int)(movementInputScroll * 10));
        }

        else if (Input.GetKeyDown(GameInput.Instance.attackButton) && player.Grounded)
        {
            Debug.Log("Player is Attacking");
            if (player.Weapon is null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }
            player.StartCoroutine(HandleAttack());
        }
        else if (Input.GetKeyDown(GameInput.Instance.attackAlternateButton) && player.Grounded)
        {
            Debug.Log("Player is Attacking (alternate)");
            if (player.Weapon is null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }
            player.StartCoroutine(HandleAlternateAttack());
        }
        else if (Input.GetKeyDown(GameInput.Instance.attackSkillButton) && player.Grounded)
        {
            if (!player.Weapon.data.canSkill)
            {
                return;
            }
            if (player.Weapon == null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }
            if (player.stateController.GetIsAiming())
            {
                player.stateController.ToggleIsAiming();
                OnAimEvent?.Invoke(player.stateController.GetIsAiming());
            }
            Debug.Log("Player is Using a skill");

            player.StartCoroutine(HandleSkill());
        }
        else if (Input.GetKeyDown(GameInput.Instance.interactButton) && player.Grounded)
        {
            Debug.Log("Player is interacting");
            if (player.stateController.currentInteractables.Count == 0)
            {
                return;
            }

            Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, enemyCloseRange, enemyLayer);

            if(hitColliders.Length > 0)
            {
                return;
            }

            if (player.stateController.currentInteractables.Count == 0)
            {
                return;
            }



            IInteractable interactable = player.stateController.currentInteractables[^1];
            interactable.Interact();
        }
        else if (Input.GetKeyDown(GameInput.Instance.switchWeaponButton))
        {
            player.SwitchWeapon();
            Debug.Log("Weapon switch else if PlayerInputController.cs"); 
        }
    }

    private void HandleToggledInputs()
    {
        if (movementInputScroll != 0)
        {
            player.CompanionSelectorIndex += (int)(movementInputScroll * 10);
            Debug.Log($"Selecting companions: {player.CompanionSelectorIndex}");
        }

        else if (Input.GetKeyDown(GameInput.Instance.attackButton))
        {
            Debug.Log("Player is Activating a companion");
            player.ActivateCompanion(player.CompanionSelectorIndex);
        }
        else if (Input.GetKeyDown(GameInput.Instance.attackAlternateButton))
        {
            Debug.Log("Player is Deactivating a companion");
            player.DeactivateCompanion(player.CompanionSelectorIndex);
        }
        else if (Input.GetKeyDown(GameInput.Instance.switchWeaponButton))
        {
            player.SwitchWeapon();
            Debug.Log("Weapon switch else if PlayerInputController.cs"); 
        }
    }

    private IEnumerator HandleAttack()
    {
        if (!player.Weapon.CanAttack)
        {
            yield return new WaitForSeconds(0);
        }

        float delay = player.Weapon.attackType switch
        {
            AttackType.MELEE => player.model.meleeAnimationDelay,
            AttackType.RANGED => player.model.rangedAnimationDelay,
            _ => 0
        };


        player.animationController.AnimateAttack(player.Weapon.attackType);
        if (player.Weapon.CanAttack)
        {
            if (player.Weapon.attackType == AttackType.MELEE
                || player.stateController.weaponState != WeaponState.ATTACK)
            {
                player.audioController.Play(PlayerAudioController.ATTACK_KEY);
            }
        }

        TriggerWeaponState(WeaponState.ATTACK);
        yield return new WaitForSeconds(delay);
        player.Weapon.Attack();
        GameStatistics.Instance.AddShotsFired();
    }

    private IEnumerator HandleAlternateAttack()
    {
        if (!player.Weapon.CanAttack)
        {
            yield return new WaitForSeconds(0);
        }

        float delay = player.Weapon.alternateAttackType switch
        {
            AttackType.MELEE => player.model.meleeAnimationDelay,
            AttackType.RANGED => player.model.rangedAnimationDelay,
            _ => 0
        };


        player.animationController.AnimateAttack(player.Weapon.alternateAttackType);
        if (player.Weapon.CanAttack)
        {
            if (player.Weapon.alternateAttackType == AttackType.MELEE
                || player.stateController.weaponState != WeaponState.ALTERNATE_ATTACK)
            {
                player.audioController.Play(PlayerAudioController.ATTACK_KEY);
            }
        }

        TriggerWeaponState(WeaponState.ALTERNATE_ATTACK);
        yield return new WaitForSeconds(delay);
        player.Weapon.AlternateAttack();
        GameStatistics.Instance.AddShotsFired();
    }

    private IEnumerator HandleSkill()
    {
        player.audioController.Play(PlayerAudioController.SKILL_KEY);
        player.animationController.AnimateSkill();
        player.Damageable = false;

        TriggerWeaponState(WeaponState.SKILL);
        yield return new WaitForSeconds(player.model.skillAnimationDelay);
<<<<<<< HEAD
        player.Weapon.Skill();
<<<<<<< HEAD
        GameStatistics.Instance.AddSkillsUsed();
=======

        Quaternion flatRotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
        player.transform.rotation = flatRotation;
>>>>>>> bcbd8415 (feat: made player prefab, minor bug fixes)
=======

        if (player.Weapon.Skill())
        {
            Quaternion flatRotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
            player.transform.rotation = flatRotation;

<<<<<<< HEAD
            GameStatisticsManager.Instance.AddSkillsUsed();
=======
            GameStatistics.Instance.AddSkillsUsed();
            player.Damageable = true;
>>>>>>> b4e37aa1 (fix: camera, skill)
        }
>>>>>>> 7e542b2c (feat: responsive crosshair and pause menu)
    }


    private IEnumerator HandleAttackWindow(float attackWindow)
    {
        yield return new WaitForSeconds(attackWindow);
        player.stateController.ClearWeaponState();
    }

    private void TriggerWeaponState(WeaponState state)
    {
        player.stateController.SetWeaponState(state);
        if (attackWindowCoroutine != null)
        {
            player.StopCoroutine(attackWindowCoroutine);
        }

        float delay = state switch
        {
            WeaponState.ATTACK => player.Weapon.data.attackInterval,
            WeaponState.ALTERNATE_ATTACK => player.Weapon.data.alternateAttackInterval,
            WeaponState.SKILL => player.Weapon.data.skillInterval,
            _ => 0
        };

        attackWindowCoroutine = player.StartCoroutine(HandleAttackWindow(delay + attackWindowSize));
    }

    // Debug purposes
    public void VisualizeEnemyRange(Player player)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.transform.position, enemyCloseRange);
    }
}