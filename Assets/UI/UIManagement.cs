using UnityEngine;
using UnityEngine.UIElements;

public class UIManagement : MonoBehaviour
{

    public static class USSAnimationClasses
    {
        static public string TransformTransition = "transform-transition-container";
        static public string Exiting = "exiting";
        static public string Entering = "entering";
        static public string ResetTranslation = "no-translation";
        static public string Hidden = "hidden";
        static public string Flex = "flex";

    }

    // Utils function
    public static VisualElement GetInnerContainer(UIDocument mainMenuDocument)
    {
        return mainMenuDocument.rootVisualElement.Query<VisualElement>("Container");
    }

    public static string GetDocumentName(UIDocument uiDocument)
    {
        return uiDocument.ToString().Split(" ")[0];
    }

    public static void ToggleElementVisible(VisualElement element, bool isVisible = true)
    {
        element.AddToClassList(isVisible ? USSAnimationClasses.Flex : USSAnimationClasses.Hidden);
        element.RemoveFromClassList(isVisible ? USSAnimationClasses.Hidden : USSAnimationClasses.Flex);

    }
}