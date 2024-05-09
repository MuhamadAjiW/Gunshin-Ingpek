using System.Collections.Generic;
using UnityEngine.UIElements;

public class TextElementWithClassAndName : TextElement
{
    public TextElementWithClassAndName(string name, List<string> classes)
    {
        this.name = name;
        classes.ForEach((className) =>
        {
            AddToClassList(className);
        });
    }

    public TextElementWithClassAndName(List<string> classes)
    {
        this.name = name;
        classes.ForEach((className) =>
        {
            AddToClassList(className);
        });
    }
}