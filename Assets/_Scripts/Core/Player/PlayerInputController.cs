using System;
using System.Collections;
using _Scripts.Core.Game.Data;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerInputController
{
    // Attributes
    private Player player;
    [HideInInspector] public Vector2 movementInput;
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

        GameInput.Instance.BindCallback(GameInput.Instance.JumpAction, Jump);
        GameInput.Instance.BindCallback(GameInput.Instance.AimAction, Aim);
        GameInput.Instance.BindCallback(GameInput.Instance.FireAction, Attack);
        GameInput.Instance.BindCallback(GameInput.Instance.AlternateFireAction, AlternateAttack);
        GameInput.Instance.BindCallback(GameInput.Instance.SkillAction, Skill);
        GameInput.Instance.BindCallback(GameInput.Instance.InteractAction, Interact);
        GameInput.Instance.BindCallback(GameInput.Instance.WeaponSwitchAction, SwitchWeapon);

    }

    // Functions
    public void HandleInputs()
    {
        movementInput = GameInput.Instance.MoveAction.ReadValue<Vector2>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(GameController.Instance.stateController.GetState() == GameState.RUNNING 
            && player.Grounded)
        {
            OnJumpEvent?.Invoke();
        }
    }

    private void Aim(InputAction.CallbackContext context)
    {
        if (GameController.Instance.stateController.GetState() != GameState.RUNNING)
        {
            return;
        }
        
        player.stateController.ToggleIsAiming();
        OnAimEvent?.Invoke(player.stateController.GetIsAiming());
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if(GameController.Instance.stateController.GetState() != GameState.RUNNING
         || !player.Grounded 
         || player.Weapon == null 
         || !player.Weapon.CanAttack)
        {
            return;
        }
        player.StartCoroutine(HandleAttack());
    }

    private void AlternateAttack(InputAction.CallbackContext context)
    {
        if(GameController.Instance.stateController.GetState() != GameState.RUNNING
        || !player.Grounded 
        || player.Weapon == null 
        || !player.Weapon.CanAttack)
        {
            return;
        }
        player.StartCoroutine(HandleAlternateAttack());
    }

    private void Skill(InputAction.CallbackContext context)
    {
        if(GameController.Instance.stateController.GetState() != GameState.RUNNING
        || !player.Grounded 
        || player.Weapon == null 
        || !player.Weapon.data.canSkill 
        || !player.Weapon.CanAttack)
        {
            return;
        }
        if (player.stateController.GetIsAiming())
        {
            player.stateController.ToggleIsAiming();
            OnAimEvent?.Invoke(player.stateController.GetIsAiming());
        }
<<<<<<< HEAD
        else if (Input.GetKeyDown(GameInput.Instance.switchWeaponButton))
        {
            player.SwitchWeapon();
            Debug.Log("Weapon switch else if PlayerInputController.cs"); 
        }
=======
        player.StartCoroutine(HandleSkill());
>>>>>>> 1cc993db (feat: overhauled inputs)
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(GameController.Instance.stateController.GetState() != GameState.RUNNING
        || !player.Grounded 
        || player.stateController.GetIsAiming() 
        || player.stateController.currentInteractables.Count == 0)
        {
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, enemyCloseRange, enemyLayer);
        if (hitColliders.Length > 0)
        {
            return;
        }
<<<<<<< HEAD
        else if (Input.GetKeyDown(GameInput.Instance.switchWeaponButton))
        {
            player.SwitchWeapon();
            Debug.Log("Weapon switch else if PlayerInputController.cs"); 
        }
=======

        IInteractable interactable = player.stateController.currentInteractables[^1];
        interactable.Interact();
>>>>>>> 1cc993db (feat: overhauled inputs)
    }

    private void SwitchWeapon(InputAction.CallbackContext context)
    {
        if(GameController.Instance.stateController.GetState() != GameState.RUNNING)
        {
            return;
        }
        player.EquipWeapon(player.WeaponIndex + 1);
    }


    // Coroutines
    private IEnumerator HandleAttack()
    {
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