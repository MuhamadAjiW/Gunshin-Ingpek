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
            Vector3 direction = MathUtils.GetDirectionVectorFlat(GameController.Instance.player.transform.position, pet.Owner.CompanionController.transform.position);
            Vector3 to = pet.Owner.CompanionController.transform.position - Vector3.Normalize(direction) * pet.stateController.maxDistFromOwner;

            Transform target = new GameObject().transform;
            target.position = to;
            GoToward(target);
        }
        else // AI_IDLE_STATE
        {
            GoToward(pet.Owner.CompanionController.transform);
        }
    }
}