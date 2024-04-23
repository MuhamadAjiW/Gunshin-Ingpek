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


        if(Input.GetKey(GameInput.instance.inputToggleButton)){
            Debug.Log("Player control is Toggled");
            if(movementInputScroll != 0)
            {
                Debug.Log("Player is scrolling in toggle");
            }
        }
        else if(movementInputScroll != 0)
        {
            player.EquipWeapon(player.WeaponIndex + (int)(movementInputScroll * 10));
        }


        else if(Input.GetKeyDown(GameInput.instance.attackButton))
        {
            Debug.Log("Player is Attacking");
            if(player.Weapon == null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }
            Debug.Log("Attacking using Weapon");

            player.Weapon.Attack();
        }
        else if(Input.GetKeyDown(GameInput.instance.attackAlternateButton))
        {
            Debug.Log("Player is Attacking (alternate)");
            if(player.Weapon == null)
            {
                Debug.Log("Player does not have a weapon");
                return;
            }

            player.Weapon.AlternateAttack();
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