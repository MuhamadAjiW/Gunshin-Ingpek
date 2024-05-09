using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopController : InGameUIScreenController
{

    public Button BuyButton;

    public Button ExitShopButton;

    public PetCatalog PetCatalog;

    public TextElement NoPetSelectedText;

    public CurrencyContainer currencyContainer;



    public void Awake()
    {
    }


    public new void OnEnable()
    {
        base.OnEnable();

        BuyButton = rootElement.Query<Button>("BuyButton");
        ExitShopButton = rootElement.Query<Button>("ExitShopButton");

        PetCatalog = rootElement.Query<PetCatalog>();

        currencyContainer = rootElement.Query<CurrencyContainer>();

        currencyContainer.dataSource = GameSaveManager.Instance.GetActiveGameSave().currencyData;

        NoPetSelectedText = rootElement.Query<TextElement>("NoPetSelectedLabel");

        BuyButton.RegisterCallback((ClickEvent evt) =>
        {
            if (PetCatalog.selectedCompanionType is null)
            {
                NoPetSelectedText.style.visibility = Visibility.Visible;
                return;
            }
            NoPetSelectedText.style.visibility = Visibility.Hidden;
            Companion.Type newCompanionType = (Companion.Type)PetCatalog.selectedCompanionType;

            Companion newCompanion = Companion.NewCompanionByType(newCompanionType);

            GameController.Instance.player.AddCompanion(newCompanion);
            GameSaveManager.Instance.GetActiveGameSave().currencyData.AddTransaction(10, "Buy pet");

        });

        ExitShopButton.RegisterCallback((ClickEvent evt) =>
        {
            GameController.Instance.stateController.PopState();
        });

    }
}
