using System.Collections.Generic;
using UnityEngine.UIElements;

public class VisualElementWithClassAndName : VisualElement
{
    public VisualElementWithClassAndName(string name, List<string> classes)
    {
        this.name = name;
        classes.ForEach((className) =>
        {
            this.AddToClassList(className);
        });
    }
}