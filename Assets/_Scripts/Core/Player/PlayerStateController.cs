using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.Animations;
=======
using UnityEngine.SceneManagement;
>>>>>>> ef999f90 (fix: death cutscene)

[Serializable]
public class PlayerStateController : EntityStateController
{
    // Attributes
    private Player player;
    public List<IInteractable> currentInteractables = new();
    public WeaponState weaponState = WeaponState.IDLE;

    private bool IsAiming = false;

    // Contstructor
    public void Init(Player player)
    {
        this.player = player;
        player.OnDeathEvent += OnDeath;
    }

    // Functions
    protected override int DetectState()
    {
        if (DetectJumping())
        {
            state = PlayerState.JUMPING;
        }
        else if (DetectFalling())
        {
            state = PlayerState.FALLING;
        }
        else if (DetectSprinting())
        {
            state = PlayerState.SPRINTING;
        }
        else if (DetectWalking())
        {
            state = PlayerState.WALKING;
        }
        else
        {
            state = PlayerState.IDLE;
        }

        if (DetectAttacking())
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

            if ((state & PlayerState.WALKING) > 0)
            {
                state &= ~PlayerState.WALKING;
                state |= PlayerState.IDLE | extraState;
            }
            else
            {
                state |= extraState;
            }
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
        return GameInput.Instance.MoveAction.ReadValue<Vector2>() != Vector2.zero;
    }
    private bool DetectSprinting()
    {
        return GameInput.Instance.MoveAction.ReadValue<Vector2>() != Vector2.zero && GameInput.Instance.SprintAction.ReadValue<float>() > 0;
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
    private void OnDeath()
    {
        state = PlayerState.DEAD;
        player.StartCoroutine(DeathCutscene());
    }

    public bool GetIsAiming()
    {
        return IsAiming;
    }

    public void ToggleIsAiming(bool newIsAiming)
    {
        IsAiming = newIsAiming;
    }

    public void ToggleIsAiming()
    {
        ToggleIsAiming(!IsAiming);
    }

    private IEnumerator DeathCutscene()
    {
        yield return new WaitForSeconds(4);
        GameController.Instance.StartCutscene(StoryConfig.KEY_CUTSCENE_DEATH, EndGame);
    }

    private void EndGame()
    {
        GameController.Instance.stateController.PushState(GameState.OVER);
    }
}
