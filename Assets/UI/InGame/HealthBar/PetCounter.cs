using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class PetCounter : VisualElement
{

    public static readonly string PetCounterContainerUSSClassName = "pet-counter-container";


    [SerializeField, DontCreateProperty]
    Dictionary<Companion.Type, int> m_CompanionAggregation = new();

    public Dictionary<Companion.Type, int> CompanionAggregation
    {
        get => m_CompanionAggregation;
        set
        {
            m_CompanionAggregation = value;
            Debug.Log("Pet counter companion aggregation value changed");
            Debug.Log(String.Format("[Pet counter setter] Companion Aggregation Count: {0}", value.Keys.Count));
            MarkDirtyRepaint();
            UpdatePetCounterCards();
        }
    }

    private Dictionary<Companion.Type, PetCounterCard> CompanionAggregationCards = new();

    public PetCounter()
    {
        name = "PetCounter";
        AddToClassList(PetCounterContainerUSSClassName);
        foreach (Companion.Type companionType in Enum.GetValues(typeof(Companion.Type)))
        {
            CompanionAggregationCards.Add(companionType, new PetCounterCard(companionType, 0));
            Add(CompanionAggregationCards.GetValueOrDefault(companionType));
        }
        generateVisualContent += GenerateVisualContent;
    }

    void UpdatePetCounterCards()
    {
        Debug.Log("Update pet counter cards");
        Debug.Log(String.Format("Companion Aggregation Count: {0}", CompanionAggregation.Keys.Count));
        foreach (var entry in CompanionAggregation)
        {
            Debug.Log(String.Format("Generate Visual Content Pet Counter. {0}: {1}", entry.Key, entry.Value));
            PetCounterCard card = CompanionAggregationCards.GetValueOrDefault(entry.Key);
            card.SetCount(entry.Value);
        }
    }

    void GenerateVisualContent(MeshGenerationContext context)
    {
    }
}