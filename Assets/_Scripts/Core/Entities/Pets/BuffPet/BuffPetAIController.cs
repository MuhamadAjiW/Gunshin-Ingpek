using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class BuffPetAIController : PetAIController<BuffPet>
{
    public override void Action()
    {
        int currState = pet.stateController.State;

        if (currState == BuffPetState.AI_AVOID_STATE)
        {
            // Get allowed farthest point from owner
            // Debug.Log($"{pet.id}: AI_AVOID_STATE");
            Vector3 direction = MathUtil.GetDirectionVectorFlat(GameController.Instance.player.transform.position, pet.Owner.CompanionController.transform.position);
            Vector3 to = pet.Owner.CompanionController.transform.position - Vector3.Normalize(direction) * pet.stateController.maxDistFromOwner;
            GoToward(to);
        }
        else // AI_IDLE_STATE
        {
            // Debug.Log($"{pet.id}: IDLE");
            GoToward(pet.Owner.CompanionController.transform);
        }
    }
}