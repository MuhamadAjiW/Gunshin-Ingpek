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
        switch (GoonState.GetAIState(goon.stateController.State))
        {
            case GoonState.AI_DETECTED_STATE:
                nav.destination = GameController.Instance.player.Position;
                break;
            case GoonState.AI_IN_RANGE_STATE:
                Vector3 direction = MathUtils.GetDirectionVectorFlat(GameController.Instance.player.Position, goon.Position);
                Quaternion look = Quaternion.LookRotation(direction);
                goon.transform.rotation = Quaternion.Slerp(look, goon.transform.rotation, Time.deltaTime);

                if(Quaternion.Angle(look, goon.transform.rotation) < 10)
                {
                    goon.Weapon.Attack();
                }
                break;
        }

    }
}