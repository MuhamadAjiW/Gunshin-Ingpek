using System;
using System.Collections;
using UnityEngine;

public class PlayerInputController
{
    // Attributes
    private readonly Player player;
    public float movementInputX;
    public float movementInputZ;
    public float movementInputScroll;
    public bool movementInputJump;
    protected Coroutine attackWindowCoroutine;

    // Events
    public event Action<float, float> OnMovementEvent;
    public event Action OnJumpEvent;
    public event Action<bool> OnAimEvent;
    private bool aim = false;
    
    // Constructor
    public PlayerInputController(Player player)
    {
        this.player = player;
    }

    // Functions
    public void HandleInputs()
    {
        movementInputX = Input.GetAxisRaw("Horizontal");
        movementInputZ = Input.GetAxisRaw("Vertical");
        movementInputScroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if(movementInputX != 0 || movementInputZ != 0)
        {
            OnMovementEvent?.Invoke(movementInputX, movementInputZ);
        }

        if(Input.GetButtonDown("Jump") && player.Grounded)
        {
            OnJumpEvent?.Invoke();
        }

        if (Input.GetKeyDown(GameInput.Instance.aimToggleButton))
        {
            aim = !aim;
            OnAimEvent?.Invoke(aim);
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
        if(movementInputScroll != 0)
        {
            player.EquipWeapon(player.WeaponIndex + (int)(movementInputScroll * 10));
        }

        else if(Input.GetKeyDown(GameInput.Instance.attackButton) && player.Grounded)
        {
            Debug.Log("Player is Attacking");
            if(player.Weapon == null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }

            player.stateController.SetWeaponState(WeaponState.ATTACK);
            
            player.StartCoroutine(HandleAttack());
        }
        else if(Input.GetKeyDown(GameInput.Instance.attackAlternateButton) && player.Grounded)
        {
            Debug.Log("Player is Attacking (alternate)");
            if(player.Weapon == null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }

            player.stateController.SetWeaponState(WeaponState.ALTERNATE_ATTACK);

            player.StartCoroutine(HandleAlternateAttack());
        }
        else if(Input.GetKeyDown(GameInput.Instance.attackSkillButton) && player.Grounded)
        {
            if(!player.Weapon.data.canSkill)
            {
                return;
            }
            if(player.Weapon == null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }
            if(aim)
            {
                aim = !aim;
                OnAimEvent?.Invoke(aim);
            }
            Debug.Log("Player is Using a skill");

            player.animationController.AnimateSkill();

            player.stateController.SetWeaponState(WeaponState.SKILL);
            player.StartCoroutine(HandleSkill());
        }
        else if(Input.GetKeyDown(GameInput.Instance.interactButton) && player.Grounded)
        {
            Debug.Log("Player is interacting");
            if(player.stateController.currentInteractables.Count == 0)
            {
                return;
            }

            IInteractable interactable = player.stateController.currentInteractables[player.stateController.currentInteractables.Count - 1];
            interactable.Interact();
        }
    }

    private void HandleToggledInputs()
    {
        if(movementInputScroll != 0)
        {
            player.CompanionSelectorIndex += (int)(movementInputScroll * 10);
            Debug.Log($"Selecting companions: {player.CompanionSelectorIndex}");
        }

        else if(Input.GetKeyDown(GameInput.Instance.attackButton))
        {
            Debug.Log("Player is Activating a companion");
            player.ActivateCompanion(player.CompanionSelectorIndex);
        }
        else if(Input.GetKeyDown(GameInput.Instance.attackAlternateButton))
        {
            Debug.Log("Player is Deactivating a companion");
            player.DeactivateCompanion(player.CompanionSelectorIndex);
        }
    }

    private IEnumerator HandleAttack()
    {
        float delay = player.Weapon.attackType switch
        {
            AttackType.MELEE => player.model.meleeAnimationDelay,
            AttackType.RANGED => player.model.rangedAnimationDelay,
            _ => 0
        };

        if(attackWindowCoroutine != null)
        {
            player.StopCoroutine(attackWindowCoroutine);
        }
        attackWindowCoroutine = player.StartCoroutine(HandleAttackWindow(player.Weapon.data.attackInterval));
        
        player.animationController.AnimateAttack(player.Weapon.attackType);
        yield return new WaitForSeconds(delay);
        player.Weapon.Attack();
    }

    private IEnumerator HandleAlternateAttack()
    {
        float delay = player.Weapon.alternateAttackType switch
        {
            AttackType.MELEE => player.model.meleeAnimationDelay,
            AttackType.RANGED => player.model.rangedAnimationDelay,
            _ => 0
        };

        if(attackWindowCoroutine != null)
        {
            player.StopCoroutine(attackWindowCoroutine);
        }
        attackWindowCoroutine = player.StartCoroutine(HandleAttackWindow(player.Weapon.data.alternateAttackInterval));

        player.animationController.AnimateAttack(player.Weapon.alternateAttackType);
        yield return new WaitForSeconds(delay);
        player.Weapon.AlternateAttack();
    }

    private IEnumerator HandleSkill()
    {
        yield return new WaitForSeconds(player.model.skillAnimationDelay);
        player.Weapon.Skill();
    }


    private IEnumerator HandleAttackWindow(float attackWindow)
    {
        yield return new WaitForSeconds(attackWindow);
        player.stateController.ClearWeaponState();
    }
}