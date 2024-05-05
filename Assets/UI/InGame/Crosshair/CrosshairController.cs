using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CrosshairController : InGameUIScreenController
{

    public GameController GameController;

    public Player player;

    public enum CrosshairType
    {
        PRIMARY,
        SECONDARY,
        TERTIARY
    }

    public void Awake()
    {
        player.inputController.OnAimEvent += ToggleCrosshair;
        player.OnWeaponChangeEvent += ToggleCrosshairOnWeapon;
    }


    public new void OnEnable()
    {
        base.OnEnable();
        ToggleCrosshair(false);
    }


    public void ToggleCrosshair(bool visible)
    {
        VisualElement crosshairContainer = rootElement.Query("CrosshairContainer").First();
        if (visible)
        {
            crosshairContainer.RemoveFromClassList(UIManagement.USSAnimationClasses.Hidden);
            crosshairContainer.AddToClassList(UIManagement.USSAnimationClasses.Flex);
        }
        else
        {
            crosshairContainer.RemoveFromClassList(UIManagement.USSAnimationClasses.Flex);
            crosshairContainer.AddToClassList(UIManagement.USSAnimationClasses.Hidden);
        }
    }

    public void ToggleCrosshairOnWeapon(int index)
    {

        string crosshairName = CrosshairType.PRIMARY.ToString();
        crosshairName = index switch
        {
            0 => CrosshairType.TERTIARY.ToString(),
            1 => CrosshairType.PRIMARY.ToString(),
            2 => CrosshairType.SECONDARY.ToString(),
            _ => CrosshairType.PRIMARY.ToString(),
        };
        List<VisualElement> crosshairsToHid = rootElement.Query(className: "crosshair").Where(crosshair => crosshair.name != crosshairName).ToList();
        crosshairsToHid.ForEach((crosshair) =>
        {
            crosshair.RemoveFromClassList(UIManagement.USSAnimationClasses.Flex);
            crosshair.AddToClassList(UIManagement.USSAnimationClasses.Hidden);
        });

        VisualElement crosshairToShow = rootElement.Query(className: "crosshair").Where(crosshair => crosshair.name == crosshairName).First();
        crosshairToShow.RemoveFromClassList(UIManagement.USSAnimationClasses.Hidden);
        crosshairToShow.AddToClassList(UIManagement.USSAnimationClasses.Flex);

    }
}
