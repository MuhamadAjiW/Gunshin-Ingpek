using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

[UxmlElement]
public partial class PetCatalog : GroupBox
{

    public static readonly string PetCatalogContainerUSSClassName = "pet-catalog-container";

    public static readonly string PetCatalogRadioButtonUSSClassName = "pet-catalog-radio-button";


    public Dictionary<Companion.Type, RadioButton> radioButtons = new();

    public Companion.Type? selectedCompanionType;

    public PetCatalog()
    {
        name = "PetCatalog";
        AddToClassList(PetCatalogContainerUSSClassName);

        foreach (Companion.Type type in new List<Companion.Type> { Companion.Type.DAMAGE, Companion.Type.HEALING })
        {
            RadioButton radioButton = new(Companion.GetCompanionTypeNameFromEnum(type));
            radioButtons.Add(type, radioButton);
            radioButton.RegisterCallback((ClickEvent evt) =>
            {
                selectedCompanionType = type;
            });
            radioButton.AddToClassList(PetCatalogRadioButtonUSSClassName);
            Add(radioButtons.GetValueOrDefault(type));
        }





        generateVisualContent += GenerateVisualContent;
    }

    void GenerateVisualContent(MeshGenerationContext context)
    {

    }
}