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

        if(Input.GetButtonDown("Jump") && player.Grounded)
        {
            OnJumpEvent?.Invoke();
        }
        if(movementInputScroll != 0)
        {
            player.EquipWeapon(player.WeaponIndex + (int)(movementInputScroll * 10));
        }

        if(Input.GetKeyDown(GameInput.instance.attackButton))
        {
            Debug.Log("Player is Attacking");
            if(player.Weapon == null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }
            Debug.Log("Attacking using Weapon");

            player.Weapon.Attack();
            (player.Weapon as TestWeapon).AlternateAttack();
        }
        else if(Input.GetKeyDown(GameInput.instance.attackAlternateButton))
        {
            Debug.Log("Player is Attacking (alternate)");
            if(player.Weapon == null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }

            (player.Weapon as TestWeapon).AlternateAttack();
        }
        else if(Input.GetKeyDown(GameInput.instance.interactButton))
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
}