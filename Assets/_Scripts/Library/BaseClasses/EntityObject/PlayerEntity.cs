using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CombatantEntity, IAccompaniable
{
    // Attributes
    public List<Companion> companionList = new();
    public List<bool> companionActive = new();
    private int companionSelectorIndex;

<<<<<<< HEAD
<<<<<<< HEAD
=======
    protected event Action OnCompanionListChange;
=======
    public event Action OnCompanionListChange;
    public event Action OnCompanionAggregationChange;

>>>>>>> dcc146fa (feat: initial work on pet counter)

>>>>>>> 42daf667 (feat: added companion aggregation)
    // Set-Getters
    public List<Companion> CompanionList => companionList;
    public List<bool> CompanionActive => companionActive;
    public MonoBehaviour CompanionController => this;

    public Dictionary<Companion.Type, int> CompanionAggregation;

    public new void Start()
    {
        base.Start();
        UpdateCompanionAggregation();
        OnCompanionListChange += UpdateCompanionAggregation;
    }


    public int CompanionSelectorIndex
    {
        get => companionSelectorIndex;
        set
        {
            // Switch requires a constant, so can't use that here
            if (value == companionList.Count) companionSelectorIndex = 0;
            else if (value == -1) companionSelectorIndex = companionList.Count - 1;
            else if (-1 < value && value < companionList.Count) companionSelectorIndex = value;
            else companionSelectorIndex = 0;
        }
    }

    // Functions
    public void ActivateCompanion(int index)
    {
        if (companionList.Count == 0)
        {
            return;
        }
        CompanionSelectorIndex = index;
        Companion selectedCompanion = CompanionList[CompanionSelectorIndex];

        if (CompanionActive[CompanionSelectorIndex])
        {
            return;
        }

        Debug.Log($"Activating Companion {selectedCompanion.name}");

        // To handle prefabs
        if (!selectedCompanion.gameObject.scene.IsValid())
        {
            selectedCompanion = ObjectFactory.CreateObject<Companion>(
                prefabPath: selectedCompanion.data.prefabPath,
                parent: EntityManager.Instance.transform,
                objectName: selectedCompanion.name
            );
            CompanionList[CompanionSelectorIndex] = selectedCompanion;
        }

        selectedCompanion.gameObject.SetActive(true);

<<<<<<< HEAD
        selectedCompanion.transform.position = transform.position - new Vector3((index + 1) * 1.2f, 0, 0);
=======
        // TODO: Set possible spawn locations for companions, for now it will spawn on the left
        selectedCompanion.transform.position = transform.position - new Vector3(-0.5f, 0, 0);
>>>>>>> 42daf667 (feat: added companion aggregation)
        companionActive[CompanionSelectorIndex] = true;
        selectedCompanion.Assign(this);
    }

    public void DeactivateCompanion(int index)
    {
        if (companionList.Count == 0)
        {
            return;
        }
        CompanionSelectorIndex = index;
        Companion selectedCompanion = CompanionList[CompanionSelectorIndex];
        companionActive[CompanionSelectorIndex] = false;
        selectedCompanion.gameObject.SetActive(false);

        Debug.Log($"Deactivating Companion {selectedCompanion.name}");
    }

    public void AddCompanion(Companion companion)
    {
        CompanionList.Add(companion);
        CompanionActive.Add(true);
        OnCompanionListChange?.Invoke();
    }

    public void DeleteCompanion(int index)
    {
        CompanionActive.RemoveAt(index);
        CompanionList.RemoveAt(index);
        OnCompanionListChange?.Invoke();
    }

    public void UpdateCompanionAggregation()
    {
        Dictionary<Companion.Type, int> aggregation = new();

        companionList.ForEach(companion =>
        {
            Companion.Type type = companion.type;
            if (aggregation.ContainsKey(type))
            {
                int oldValue = aggregation.GetValueOrDefault(type);
                aggregation.Remove(type);
                aggregation.Add(type, oldValue + 1);

            }
            else
            {
                aggregation.Add(type, 1);
            }
        });

        CompanionAggregation = aggregation;
        OnCompanionAggregationChange?.Invoke();

    }

    public void ActivateAllCompanions()
    {
        for (int i = 0; i < companionList.Count; i++)
        {
            ActivateCompanion(i);
        }
    }
}
