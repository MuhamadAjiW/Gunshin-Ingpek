using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data.Currency;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopController : InGameUIScreenController
{

    public Button BuyButton;

    public Button ExitShopButton;

    public PetCatalog PetCatalog;

    public PetCounter PetCounter;


    public TextElement NoPetSelectedText;

    public CurrencyContainer currencyContainer;

    // public CurrencyData currencyData;


    public void Awake()
    {
    }

    public new void Start()
    {
        base.Start();

        BuyButton = rootElement.Query<Button>("BuyPetButton");
        ExitShopButton = rootElement.Query<Button>("ExitShopButton");

        PetCatalog = rootElement.Query<PetCatalog>();

        currencyContainer = rootElement.Query<CurrencyContainer>();

        Debug.Assert(GameSaveManager.Instance != null);
        currencyContainer.dataSource = GameSaveManager.Instance.GetActiveGameSave().currencyData;

        // currencyData = new();
        // currencyContainer.dataSource = currencyData;

        NoPetSelectedText = rootElement.Query<TextElement>("NoPetSelectedLabel");

        BuyButton.RegisterCallback((ClickEvent evt) =>
        {
            if (PetCatalog.selectedCompanionType is null)
            {
                return;
            }
            NoPetSelectedText.style.visibility = Visibility.Hidden;
            Companion.Type newCompanionType = (Companion.Type)PetCatalog.selectedCompanionType;

            Companion newCompanion = Companion.NewCompanionByType(newCompanionType);

            GameController.Instance.player.AddCompanion(newCompanion, newCompanionType);

            int price = GameController.Instance.cheatController.MOTHERLODE ? 0 : 10;
            Debug.Log($"Price of companion: {price}");

            GameSaveManager.Instance.GetActiveGameSave().currencyData.AddTransaction(-price, "Buy pet");
            Debug.Log(String.Format("Number of companion ${0}", GameController.Instance.player.CompanionList.Count));

        });

        ExitShopButton.RegisterCallback((ClickEvent evt) =>
        {
            GameController.Instance.stateController.PopState();
        });

        PetCounter = rootElement.Query<PetCounter>();

        player.OnCompanionAggregationChange += () =>
        {
            Debug.Log("Companion aggregation in health bar controller changed");
            Debug.Log(String.Format("[Health Bar controller] Companion Aggregation Count: {0}", player.CompanionAggregation.Keys.Count));
            PetCounter.CompanionAggregation = player.CompanionAggregation;
        };

        PetCatalog.OnSelectedCompanionTypeChange += ToggleBuyButtonActive;

        VisualElement container = UIManagement.GetInnerContainer(ControlledUIDocument);

        container.RegisterCallback((ClickEvent evt) =>
        {
            PetCatalog.ResetSelectedPetType();
        });



    }

    void ToggleBuyButtonActive(Companion.Type? selectedType)
    {
        BuyButton.SetEnabled(selectedType is not null);

        if (selectedType is null)
        {
            BuyButton.RemoveFromClassList("do-button");
            BuyButton.AddToClassList("disabled");
        }
        else
        {
            BuyButton.AddToClassList("do-button");
            BuyButton.RemoveFromClassList("disabled");
        }
    }
}
