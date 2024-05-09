using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CrosshairController : InGameUIScreenController
{

    public enum CrosshairType
    {
        NOWEAPON,
        RIFLE,
        RIFLEHIGHDAMAGE,
        SHOTGUN,
        SWORD

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

        string crosshairName = index switch
        {
            0 => CrosshairType.NOWEAPON.ToString(),
            1 => CrosshairType.RIFLE.ToString(),
            2 => CrosshairType.RIFLEHIGHDAMAGE.ToString(),
            3 => CrosshairType.SHOTGUN.ToString(),
            4 => CrosshairType.SWORD.ToString(),
            _ => CrosshairType.NOWEAPON.ToString(),
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
