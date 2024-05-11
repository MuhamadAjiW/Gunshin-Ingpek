using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class CurrencyContainer : VisualElement
{

    public static readonly string CurrencyContainerUSSClassName = "currency-container";


    public TextElementWithClassAndName currencyLabel = new("CurrencyLabel", new List<string> { "currency-label" });

    public TextElementWithClassAndName currencyLabelIcon = new("CurrencyLabelIcon", new List<string> { "currency-label-icon" });

    public int m_currency;

    [UxmlAttribute, CreateProperty]
    public int Currency
    {
        get => m_currency;
        set
        {
            m_currency = value;
            UpdateText();
        }
    }

    public CurrencyContainer()
    {
        name = "CurrencyContainer";

        currencyLabel.text = 0.ToString();
        currencyLabelIcon.text = "🪙";
        Add(currencyLabelIcon);
        Add(currencyLabel);

        generateVisualContent += GenerateVisualContent;
    }

    void UpdateText()
    {
        if (GameController.Instance != null && GameController.Instance.cheatController.MOTHERLODE)
        {
            currencyLabel.text = "♾️";
            return;
        }

        currencyLabel.text = Currency.ToString();
    }


    void GenerateVisualContent(MeshGenerationContext context)
    {
    }
}