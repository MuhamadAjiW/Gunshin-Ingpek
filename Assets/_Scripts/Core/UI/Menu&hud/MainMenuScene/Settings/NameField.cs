using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class NameField : TextField
{

    public void SetName(string value)
    {
        text = value;
    }

}