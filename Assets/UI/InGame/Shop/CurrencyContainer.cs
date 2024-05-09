using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.UIElements;

[UxmlElement]
public partial class CurrencyContainer : VisualElement
{

    public static readonly string CurrencyContainerUSSClassName = "currency-container";


    public TextElementWithClassAndName currencyLabel = new("CurrencyLabel", new List<string> { "currency-label" });

    public int m_currency;

    [UxmlAttribute, CreateProperty]
    public int Currency
    {
        get => m_currency;
        set
        {
            m_currency = value;
            MarkDirtyRepaint();
        }
    }

    public CurrencyContainer()
    {
        name = "CurrencyContainer";

        currencyLabel.text = 0.ToString();
        Add(currencyLabel);

        generateVisualContent += GenerateVisualContent;
    }

    void GenerateVisualContent(MeshGenerationContext context)
    {
        currencyLabel.text = Currency.ToString();
    }
}