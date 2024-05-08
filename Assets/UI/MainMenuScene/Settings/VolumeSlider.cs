using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class VolumeSlider : Slider
{
    public VolumeSlider()
    {
        lowValue = 0;
        highValue = 120;
        value = 100;
        generateVisualContent += GenerateVisualContent;
    }

    void GenerateVisualContent(MeshGenerationContext context)
    {
    }
}