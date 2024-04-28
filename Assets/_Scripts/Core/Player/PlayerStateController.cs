using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateController : EntityStateController
{
    // Attributes
    private readonly Player player;
    public List<IInteractable> currentInteractables = new();
    public WeaponState weaponState = WeaponState.IDLE;

    // Contstructor
    public PlayerStateController(Player player)
    {
        this.player = player;
        player.OnDeathEvent += OnDeath;
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
            AttackType attackType = weaponState switch
            {
                WeaponState.ATTACK => player.Weapon.attackType,
                WeaponState.ALTERNATE_ATTACK => player.Weapon.alternateAttackType,
                _ => AttackType.NULL
            };

            int extraState = attackType switch
            {
                AttackType.RANGED => PlayerState.ATTACK_RANGED,
                AttackType.MELEE => PlayerState.ATTACK_MELEE,
                _ => PlayerState.NULL
            };

            state |= extraState;
        }

        if(initialState != state)
        {
            InvokeOnStateChanged(initialState);
        }

        return state;
    }

    public void ClearWeaponState()
    {
        weaponState = WeaponState.IDLE;
    }
    public void SetWeaponState(WeaponState state)
    {
        weaponState = state;
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
        return weaponState != WeaponState.IDLE;
    }
    private void OnDeath()
    {
        state = PlayerState.DEAD;
    }
}
