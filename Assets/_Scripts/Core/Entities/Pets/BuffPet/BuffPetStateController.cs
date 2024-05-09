using System;
using UnityEngine;

[Serializable]
public class BuffPetStateController : PetStateController<BuffPet>
{
    // Attributes
    public float avoidDistance = 7f;
    public float maxDistFromOwner = 5;

    // Functions
    protected override int DetectState()
    {
        if (Vector3.Distance(pet.transform.position, GameController.Instance.player.transform.position) <= avoidDistance)
        {
            state = BuffPetState.AI_AVOID_STATE;
        }
        else
        {
            state = BuffPetState.AI_IDLE_STATE;
        }

        return state;
    }

    // Debugging functions
    public void VisualizeDetection()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(pet.transform.position, avoidDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pet.Owner.CompanionController.transform.position, maxDistFromOwner);
    }
}
