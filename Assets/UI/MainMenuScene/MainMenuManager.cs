using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] public List<UIDocument> MainMenuUIDocuments;
    [SerializeField] public List<GameObject> MainMenuUIDocumentsDirectionOnSelected;

    [SerializeField] public List<GameObject> MainMenuUIDocumentsCameraPosition;

    [SerializeField] public CameraManager CameraManager;
    public int TransitionTimeMiliSeconds;

    public float ZoomPeriodMiliSeconds;

    public float ZoomMaxPercent;

    private float ZoomDurationMiliSeconds;

    private bool IsZoomingIn = true;

    private UIDocument OpenedUIDocument;

    public string OpenedUIDocumentString
    {
        get => UIManagement.GetDocumentName(OpenedUIDocument);
    }

    public bool MainMenuTransitionInProgress = false;

    public void OnEnable()
    {
        foreach (var document in MainMenuUIDocuments)
        {
            UIManagement.GetInnerContainer(document).AddToClassList(UIManagement.USSAnimationClasses.TransformTransition);
            UIManagement.ToggleElementVisible(UIManagement.GetInnerContainer(document));

            if (UIManagement.GetDocumentName(document) != "ClickToStart")
            {
                UIManagement.GetInnerContainer(document).AddToClassList(UIManagement.USSAnimationClasses.Entering);
            }
        }
        DisplayUIDocument("ClickToStart");
    }

    public void InitializeBackButton(VisualElement rootElement)
    {
        Button backButton = rootElement.Query<Button>("back-button");
        if (backButton is null)
        {
            return;
        }

        backButton.RegisterCallback((ClickEvent evt) =>
        {
            if (!MainMenuTransitionInProgress)
            {
                GoToMainMenu(true);
            }
        });
    }

    public void GoToMainMenu(bool withTransition = false)
    {
        DisplayUIDocument("MainMenu", withTransition, true);
    }

    public void DisplayUIDocument(string displayedDocumentName, bool withTransition = false, bool reverseTransition = false)
    {
        UIDocument newDocument = MainMenuUIDocuments.Find(document => document.ToString().Split(" ")[0] == displayedDocumentName);
        if (withTransition)
        {
            StartCoroutine(ReplaceUIDocument(newDocument, reverseTransition));
        }
        else
        {
            if (OpenedUIDocument is not null)
            {
                UIManagement.ToggleElementVisible(UIManagement.GetInnerContainer(OpenedUIDocument), false);
            }
            LookAtDocument(newDocument, false);
            UIManagement.ToggleElementVisible(UIManagement.GetInnerContainer(newDocument));
            OpenedUIDocument = newDocument;
        }
    }

    int GetUIDocumentIndex(UIDocument document)
    {
        return MainMenuUIDocuments.FindIndex(documentSearched => UIManagement.GetDocumentName(document) == UIManagement.GetDocumentName(documentSearched));
    }

    void LookAtDocument(UIDocument document, bool isAsync = true)
    {
        int nthDocument = GetUIDocumentIndex(document);
        GameObject targetDirection = MainMenuUIDocumentsDirectionOnSelected[nthDocument];
        GameObject targetPosition = MainMenuUIDocumentsCameraPosition[nthDocument];

        if (targetDirection is not null && targetPosition is not null)
        {
            CameraManager.SmoothLookAt(targetDirection, targetPosition, isAsync ? TransitionTimeMiliSeconds : 0f);
        }
    }

    IEnumerator ReplaceUIDocument(UIDocument newDocument, bool reverseTransition = false)
    {
        MainMenuTransitionInProgress = true;
        VisualElement newDocumentInnerContainer = UIManagement.GetInnerContainer(newDocument);
        VisualElement oldDocumentInnerContainer = UIManagement.GetInnerContainer(OpenedUIDocument);
        if (newDocumentInnerContainer is null)
        {
            Debug.LogError("New document doesn't have inner container!");
            yield break;
        }

        if (oldDocumentInnerContainer is null)
        {
            Debug.LogError("Old document doesn't have inner container!");
            yield break;
        }

        LookAtDocument(newDocument);

        string enteringClass = !reverseTransition ? UIManagement.USSAnimationClasses.Entering : UIManagement.USSAnimationClasses.Exiting;
        string exitingClass = !reverseTransition ? UIManagement.USSAnimationClasses.Exiting : UIManagement.USSAnimationClasses.Entering;

        // Transition the new document in
        newDocumentInnerContainer.RemoveFromClassList(enteringClass);
        newDocumentInnerContainer.AddToClassList(UIManagement.USSAnimationClasses.ResetTranslation);
        newDocumentInnerContainer.AddToClassList(UIManagement.USSAnimationClasses.Flex);

        yield return null;

        // Transition the old document out
        if (OpenedUIDocument is not null)
        {
            oldDocumentInnerContainer.AddToClassList(exitingClass);
            yield return new WaitForSeconds(TransitionTimeMiliSeconds / 1000);
            oldDocumentInnerContainer.AddToClassList(UIManagement.USSAnimationClasses.Hidden);

        }

        newDocumentInnerContainer.RemoveFromClassList(UIManagement.USSAnimationClasses.ResetTranslation);


        OpenedUIDocument = newDocument;
        MainMenuTransitionInProgress = false;
        yield return null;
    }

}
