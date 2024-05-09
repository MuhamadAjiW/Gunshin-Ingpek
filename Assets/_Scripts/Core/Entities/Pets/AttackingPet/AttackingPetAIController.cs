using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class AttackingPetAIController : PetAIController<AttackingPet>
{
    public override void Action()
    {
        int currState = pet.stateController.State;

        if (currState == AttackingPetState.AI_ATTACK_STATE)
        {
            pet.aiController.nav.stoppingDistance = 5;
            // EnemyEntity target = pet.stateController.GetNearestEnemy(false);
            EnemyEntity target = pet.stateController.nearest;
            Quaternion targetAngle = LookToward(target.transform);
            if (Quaternion.Angle(targetAngle, pet.transform.rotation) < 10)
            {
                Attack();
            }
        }
        else if (currState == AttackingPetState.AI_FOLLOW_STATE)
        {
            // EnemyEntity target = pet.stateController.GetNearestEnemy(true);
            pet.aiController.nav.stoppingDistance = 5;
            EnemyEntity target = pet.stateController.nearest;
            GoToward(target.transform);
        }
        else // AI_IDLE_STATE
        {
            pet.aiController.nav.stoppingDistance = 2;
            LookToward(GameController.Instance.player.transform);
            GoToward(GameController.Instance.player.transform);
        }
    }

    public IEnumerator HandleAttack()
    {
        yield return new WaitForSeconds(1f);
        pet.Weapon.Attack();
    }

    public void Attack()
    {
        pet.StartCoroutine(HandleAttack());
    }
}