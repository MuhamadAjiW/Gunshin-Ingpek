using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Objects.Interactables;
using UnityEngine;

public class EventManager : BaseObjectManager
{
    // Static instance
    public static EventManager Instance;
    public List<Shopkeeper> Shops;
    public List<SavePoint> SavePoints;
    public List<WeaponObject> WeaponPool;

    // Constructor
    protected void Awake()
    {
        Instance = this;
        ManagerName = "Event Manager";

        for (int i = 0; i < WeaponPool.Count; i++)
        {
            WeaponPool[i].poolIndex = i;
        }
    }

    public void SetShop(int index, bool active)
    {
        if(index >= Shops.Count)
        {
            Debug.LogWarning("Shop index out of bounds");
            return;
        }

        Shops[index].SetActive(active);
    }
}
