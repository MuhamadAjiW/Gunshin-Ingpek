using UnityEngine;
using UnityEngine.UIElements;
public class ScreenController : MonoBehaviour
{
    public UIDocument ControlledUIDocument;

    protected VisualElement rootElement;

    public void OnEnable()
    {
        rootElement = ControlledUIDocument.rootVisualElement;
    }

}