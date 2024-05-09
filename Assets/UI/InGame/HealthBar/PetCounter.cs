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
            MarkDirtyRepaint();
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

    void GenerateVisualContent(MeshGenerationContext context)
    {
        foreach (var entry in CompanionAggregation)
        {
            PetCounterCard card = CompanionAggregationCards.GetValueOrDefault(entry.Key);
            card.SetCount(entry.Value);
        }
    }
}