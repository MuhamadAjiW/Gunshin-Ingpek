using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Objects.Interactables;
using UnityEngine;

public class EventManager : BaseObjectManager
{
    // Static instance
    public static EventManager Instance;
    [SerializeField] private List<Shopkeeper> Shops;
    [SerializeField] private List<SavePoint> SavePoints;

    // Constructor
    protected void Awake()
    {
        Instance = this;
        ManagerName = "Event Manager";
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
