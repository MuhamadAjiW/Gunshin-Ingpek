using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public partial class GameSaveCard : VisualElement
{

    TextElement indexText = new();
    TextElement saveTimeText = new();


    public GameSaveCard(GameSaveData gameSaveData, int index, List<string> classes)
    {
        name = string.Format("GameSaveCard-{0}", gameSaveData.id);
        classes.ForEach((className) =>
        {
            AddToClassList(className);
        });

        indexText.text = index.ToString();
        saveTimeText.text = gameSaveData.writeTime.ToString();

        Add(indexText);
        Add(saveTimeText);

        RegisterCallback((ClickEvent evt) =>
        {
            // Load the save game
            GameSaveManager.Instance?.LoadExistingSave(index);
        }
        );

    }


}