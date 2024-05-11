using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class PetCatalog : GroupBox
{

    public static readonly string PetCatalogContainerUSSClassName = "pet-catalog-container";

    public static readonly string PetCatalogOptionUSSClassName = "pet-catalog-option";

    public static readonly string PetCatalogOptionTitleUSSClassName = "pet-catalog-option-title";

    public static readonly string PetCatalogOptionDescriptionUSSClassName = "pet-catalog-option-description";

    public static readonly string PetCatalogOptionSelectedUSSClassName = "pet-catalog-option-selected";

    public Companion.Type? selectedCompanionType = Companion.Type.DAMAGE;

    public event Action<Companion.Type?> OnSelectedCompanionTypeChange;

    public List<Button> petOptions = new();

    public Dictionary<Companion.Type, string> petDescription = new() {
        {Companion.Type.DAMAGE, "Damage enemies. Come with a mini AK47."},
        {Companion.Type.HEALING, "Heals you during combat"},
    };

    public Dictionary<Companion.Type, List<string>> petOptionUSSClassNames = new() {
        {Companion.Type.DAMAGE, new() {"damage-dealer-option"}},
        {Companion.Type.HEALING, new() {"healer-option"}},
    };



    public PetCatalog()
    {
        name = "PetCatalog";
        AddToClassList(PetCatalogContainerUSSClassName);

        foreach (Companion.Type type in new List<Companion.Type> { Companion.Type.DAMAGE, Companion.Type.HEALING })
        {
            Button petOptionButton = new Button()
            {
                name = $"PetOptionButton-{type}"
            };
            TextElementWithClassAndName petOptionButtonTitle = new($"PetOptionButtonTitle-{type}", new List<string> { PetCatalogOptionTitleUSSClassName })
            {
                text = Companion.GetCompanionTypeNameFromEnum(type)
            };
            TextElementWithClassAndName petOptionButtonDescription = new($"PetOptionButtonDescription-{type}", new List<string> { PetCatalogOptionDescriptionUSSClassName })
            {
                text = petDescription.GetValueOrDefault(type)
            };

            foreach (string className in petOptionUSSClassNames.GetValueOrDefault(type))
            {
                petOptionButton.AddToClassList(className);
            }

            petOptionButton.Add(petOptionButtonTitle);
            petOptionButton.Add(petOptionButtonDescription);

            petOptions.Add(petOptionButton);
            Add(petOptionButton);

            petOptionButton.AddToClassList(PetCatalogOptionUSSClassName);
            petOptionButton.RegisterCallback((ClickEvent evt) =>
            {
                selectedCompanionType = type;
                OnSelectedCompanionTypeChange?.Invoke(type);
            });
        }

        RerenderCatalogOption(selectedCompanionType);

        OnSelectedCompanionTypeChange += RerenderCatalogOption;

        generateVisualContent += GenerateVisualContent;
    }

    void GenerateVisualContent(MeshGenerationContext context)
    {

    }

    public void RerenderCatalogOption(Companion.Type? type)
    {
        foreach (Button petOptionButton in petOptions)
        {
            Enum.TryParse(petOptionButton.name.Split("-")[1], out Companion.Type buttonType);

            if (buttonType == type)
            {
                petOptionButton.AddToClassList(PetCatalogOptionSelectedUSSClassName);
                foreach (string className in petOptionUSSClassNames.GetValueOrDefault(buttonType))
                {
                    petOptionButton.RemoveFromClassList(className);

                }
            }
            else
            {
                petOptionButton.RemoveFromClassList(PetCatalogOptionSelectedUSSClassName);
                foreach (string className in petOptionUSSClassNames.GetValueOrDefault(buttonType))
                {
                    petOptionButton.AddToClassList(className);

                }
            }

        }

    }
}