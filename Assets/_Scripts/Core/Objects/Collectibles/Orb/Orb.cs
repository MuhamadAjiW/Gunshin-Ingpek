using _Scripts.Core.Game.Data;
using UnityEngine;

public abstract class Orb : Collectible
{
    // Static Attributes
    private const string RESTORE_HEALTH_ORB_PREFAB = "Prefabs/Collectibles/Orb/RestoreHealthOrb/RestoreHealthOrb";
    private const string INCREASE_SPEED_ORB_PREFAB = "Prefabs/Collectibles/Orb/IncreaseSpeedOrb/IncreaseSpeedOrb";
    private const string INCREASE_DAMAGE_ORB_PREFAB = "Prefabs/Collectibles/Orb/IncreaseDamageOrb/IncreaseDamageOrb";

    // Functions
    protected override void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.transform.parent.gameObject.TryGetComponent<Player>(out _))
        {
            base.OnTriggerEnter(otherCollider);
        }
    }

    protected override void OnCollect()
    {
        GameStatisticsManager.Instance.AddOrbsCollected();
        Debug.Log(id + ": Collected. Current orbs collected: " + GameStatisticsManager.Instance.OrbsCollected);
    }

    protected override void OnTimeout()
    {
        Debug.Log(id + ": Timeout");
    }

    public static Orb GenerateRandomOrb(Vector3 position, string name)
    {
        int random = Random.Range(0, 3);

        return random switch
        {
            0 => ObjectFactory.CreateCollectibleObject<RestoreHealthOrb>(
                                prefabPath: RESTORE_HEALTH_ORB_PREFAB,
                                position: position,
                                objectName: name
                            ),
            1 => ObjectFactory.CreateCollectibleObject<IncreaseSpeedOrb>(
                                prefabPath: INCREASE_SPEED_ORB_PREFAB,
                                position: position,
                                objectName: name
                            ),
            _ => ObjectFactory.CreateCollectibleObject<IncreaseDamageOrb>(
                                prefabPath: INCREASE_DAMAGE_ORB_PREFAB,
                                position: position,
                                objectName: name
                            ),
        };
    }
}