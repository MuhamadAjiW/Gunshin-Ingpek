using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class GoonAIController
{
    // Attributes
    private readonly Goon goon;
    private readonly NavMeshAgent nav;

    // Constructor
    public GoonAIController(Goon goon)
    {
        this.goon = goon;
        nav = goon.GetComponent<NavMeshAgent>();
    }

    // Functions
    public void Action()
    {
        Debug.Log($"State: {GoonState.GetAIState(goon.stateController.State)}");
        switch (GoonState.GetAIState(goon.stateController.State))
        {
            case GoonState.AI_DETECTED_STATE:
                nav.destination = GameController.Instance.player.Position;
                break;
            case GoonState.AI_IN_RANGE_STATE:
                Vector3 direction = MathUtils.GetDirectionVectorFlat(GameController.Instance.player.Position, goon.Position);
                Quaternion look = Quaternion.LookRotation(direction);
                goon.transform.rotation = Quaternion.Slerp(look, goon.transform.rotation, Time.deltaTime * 4f);

                if(Quaternion.Angle(look, goon.transform.rotation) < 10)
                {
                    goon.Weapon.Attack();
                }
                break;
        }

    }
}