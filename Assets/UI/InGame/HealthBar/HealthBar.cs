using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class HealthBar : VisualElement
{

    public static readonly string OuterBarUSSClassName = "outer-bar";
    public static readonly string HealthBarUSSClassName = "health-bar";
    public static readonly string HealthBarBackgroundUSSClassName = "health-bar-background";


    [SerializeField, DontCreateProperty]
    float m_Health;

    VisualElement m_OuterBar;
    VisualElement m_HealthBar;
    VisualElement m_HealthBarBackground;



    [UxmlAttribute, CreateProperty]
    public float Health
    {
        get => m_Health;
        set
        {
            m_Health = value;
            MarkDirtyRepaint();
        }
    }

    public HealthBar()
    {
        // m_HealthBarBackground = new VisualElement
        // {
        //     name = "HealthBarBackground"
        // };
        // m_HealthBarBackground.AddToClassList(HealthBarBackgroundUSSClassName);

        m_HealthBar = new VisualElement
        {
            name = "HealthBar"
        };
        m_HealthBar.AddToClassList(HealthBarUSSClassName);
        // m_HealthBar.Add(m_HealthBarBackground);

        m_OuterBar = new VisualElement
        {
            name = "OuterBar"
        };
        m_OuterBar.AddToClassList(OuterBarUSSClassName);
        m_OuterBar.Add(m_HealthBar);
        Add(m_OuterBar);

        generateVisualContent += GenerateVisualContent;
    }

    void GenerateVisualContent(MeshGenerationContext context)
    {
        m_HealthBar.style.width = Length.Percent(Health);
        // m_HealthBarBackground.style.width = Length.Percent(Health);
    }
}