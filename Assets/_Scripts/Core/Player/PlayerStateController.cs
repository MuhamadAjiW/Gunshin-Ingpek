using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : EntityStateController
{
    // Attributes
    private readonly Player player;
    public AttackType attack = AttackType.NULL;
    public List<IInteractable> currentInteractables = new();

    // Contstructor
    public PlayerStateController(Player player)
    {
        this.player = player;
    }

    // Functions
    public override int UpdateState()
    {
        int initialState = state;

        if(DetectJumping())
        {
            state = PlayerState.JUMPING;
        }
        else if(DetectFalling())
        {
            state = PlayerState.FALLING;
        }
        else if(DetectSprinting())
        {
            state = PlayerState.SPRINTING;
        }
        else if(DetectWalking())
        {
            state = PlayerState.WALKING;
        }
        else
        {
            state = PlayerState.IDLE;
        }

        if(DetectAttacking())
        {
            int extraState = player.Weapon.AttackType switch
            {
                AttackType.RANGED => PlayerState.ATTACK_RANGED,
                AttackType.MELEE => PlayerState.ATTACK_MELEE,
                _ => PlayerState.NULL
            };

            state |= extraState;
        }

        if(initialState != state)
        {
            InvokeOnStateChanged();
        }

        return state;
    }

    private bool DetectWalking()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }
    private bool DetectSprinting()
    {
        return (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && Input.GetKey(GameInput.Instance.sprintButton);
    }
    private bool DetectJumping()
    {
        return !player.Grounded && player.Rigidbody.velocity.y > 0;
    }
    private bool DetectFalling()
    {
        return !player.Grounded && player.Rigidbody.velocity.y < 0;
    }
    private bool DetectAttacking()
    {
        return !player.Weapon.CanAttack;
    }
}
