using UnityEngine;
using UnityEngine.UIElements;
public class ScreenController : MonoBehaviour
{
    public UIDocument ControlledUIDocument;

    protected VisualElement rootElement;

    protected VisualElement InnerContainer
    {
        get => UIManagement.GetInnerContainer(ControlledUIDocument);
    }

    public void OnEnable()
    {
        rootElement = ControlledUIDocument.rootVisualElement;
    }

}