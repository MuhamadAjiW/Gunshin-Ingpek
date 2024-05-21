using System;
using System.Linq;
using UnityEngine.UIElements;

[UxmlElement]
public partial class DifficultyDropdown : DropdownField
{
    public DifficultyDropdown()
    {
        index = 1;
        choices = Enum.GetNames(typeof(DifficultyType)).ToList();
        value = choices[1];
        generateVisualContent += GenerateVisualContent;
    }

    void GenerateVisualContent(MeshGenerationContext context)
    {
    }
}