using System;
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
                goon.Weapon.Attack();
                break;
        }
    }
}