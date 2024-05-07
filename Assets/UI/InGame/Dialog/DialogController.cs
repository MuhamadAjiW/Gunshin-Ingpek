using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogController : InGameUIScreenController
{
    public static DialogController Instance;

    // Attributes
    Dialog dialogOverlay;
    CutsceneData currentCutscene;
    int cutsceneProgress;

    // Events
    public event Action OnCutsceneFinished;

    // Constructor
    public new void OnEnable()
    {
        base.OnEnable();
        Instance = this;
        dialogOverlay = rootElement.Q<Dialog>();    
    }

    public void StartCutscene(CutsceneData cutsceneData)
    {
        GameController.stateController.PushState(GameState.CUTSCENE);

        currentCutscene = cutsceneData;
        cutsceneProgress = -1;
        ProgressCutscene();
        // dialogOverlay.dataSource = currentCutscene.dialogs[cutsceneProgress];
    }

    public void ProgressCutscene()
    {
        if(cutsceneProgress < currentCutscene.dialogs.Count)
        {
            cutsceneProgress++;

            if(cutsceneProgress == currentCutscene.dialogs.Count)
            {
                OnCutsceneFinished?.Invoke();
                GameController.stateController.PopState();
            }
            else
            {
                LoadData(currentCutscene.dialogs[cutsceneProgress]);
            }
        }
    }

    public void LoadData(DialogData data)
    {
        dialogOverlay.m_Overlay.style.backgroundColor = data.OverlayColor;
        dialogOverlay.m_PersonLLabelContainer.visible = data.PersonLActive;
        dialogOverlay.m_PersonLImage.visible = data.PersonLActive;
        dialogOverlay.m_PersonLImage.style.backgroundImage = new StyleBackground(data.PersonLImage);

        dialogOverlay.m_PersonLLabelText.text = data.PersonLName;

        dialogOverlay.m_PersonRLabelContainer.visible = data.PersonRActive;
        dialogOverlay.m_PersonRImage.visible = data.PersonRActive;
        dialogOverlay.m_PersonRImage.style.backgroundImage = new StyleBackground(data.PersonRImage);
        dialogOverlay.m_PersonRLabelText.text = data.PersonRName;
        
        dialogOverlay.m_MainText.text = data.Text;
    }
}
