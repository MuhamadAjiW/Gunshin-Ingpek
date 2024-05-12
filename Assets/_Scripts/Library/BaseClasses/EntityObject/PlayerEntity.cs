using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerEntity : CombatantEntity, IAccompaniable
{
    // Attributes
    public List<Companion> companionList = new();
    public List<bool> companionActive = new();
    private int companionSelectorIndex;

    public event Action OnCompanionListChange;
    public event Action OnCompanionAggregationChange;


    // Set-Getters
    public List<Companion> CompanionList => companionList;
    public List<bool> CompanionActive => companionActive;
    public MonoBehaviour CompanionController => this;

    public Dictionary<Companion.Type, int> CompanionAggregation = new();

    public new void Start()
    {
        base.Start();
        UpdateCompanionAggregation();
        OnCompanionAggregationChange?.Invoke();
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

        selectedCompanion.spawnPosition = transform.position - new Vector3((index + 1) * 1.2f, 0, 0);
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

    public void AddCompanion(Companion companion, Companion.Type type)
    {
        companionList.Add(companion);
        companionActive.Add(false);
        ActivateAllCompanions();

        Debug.Log($"Adding companion {companion.name} with type {type}");
        OnCompanionListChange?.Invoke();

        GameSaveManager.Instance.gameSaves[GameSaveManager.Instance.activeGameSaveIndex].petData.Add(type);
        Debug.Log("Companion added");
    }

    public void DeleteCompanion(int index)
    {
        CompanionActive.RemoveAt(index);
        CompanionList.RemoveAt(index);
        GameSaveManager.Instance.gameSaves[GameSaveManager.Instance.activeGameSaveIndex].petData.RemoveAt(index);
        OnCompanionListChange?.Invoke();
        Debug.Log("Companion deleted");
    }

    public void UpdateCompanionAggregation()
    {
        Debug.Log("Companion aggregation updated");
        Dictionary<Companion.Type, int> aggregation = new();

        companionList.ForEach(companion =>
        {
            Companion.Type type = companion.type;
            Debug.Log(type);
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



        Debug.Log(String.Format("[Update companion aggregation player entity] Companion Aggregation Count: {0}", aggregation.Keys.Count));
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

    public IEnumerator KillAllCompanions()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < companionList.Count; i++)
        {
            Companion comp = companionList[i];
            companionList.RemoveAt(i);
            companionActive.RemoveAt(i);
            Destroy(comp.gameObject);
        }
    }
}
