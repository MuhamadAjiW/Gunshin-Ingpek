using System;
using UnityEngine;

public class PlayerInputController
{
    // Attributes
    private readonly Player player;
    public float movementInputX;
    public float movementInputZ;
    public float movementInputScroll;
    public bool movementInputJump;

    // Events
    public event Action<float, float> OnMovementEvent;
    public event Action OnJumpEvent;
    
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

        else if(Input.GetKeyDown(GameInput.Instance.attackButton))
        {
            Debug.Log("Player is Attacking");
            if(player.Weapon == null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }

            player.Weapon.Attack();
        }
        else if(Input.GetKeyDown(GameInput.Instance.attackAlternateButton))
        {
            Debug.Log("Player is Attacking (alternate)");
            if(player.Weapon == null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }

            player.Weapon.AlternateAttack();
        }
        else if(Input.GetKeyDown(GameInput.Instance.interactButton))
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
}