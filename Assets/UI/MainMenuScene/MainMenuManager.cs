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

    public bool MainMenuTransitionInProgress = false;

    static class USSAnimationClasses
    {
        static public string TransformTransition = "transform-transition-container";
        static public string Exiting = "exiting";
        static public string Entering = "entering";
        static public string ResetTranslation = "no-translation";

        static public string Hidden = "hidden";
        static public string Flex = "flex";

    }

    public void OnEnable()
    {
        foreach (var document in MainMenuUIDocuments)
        {
            GetInnerContainer(document).AddToClassList(USSAnimationClasses.TransformTransition);
            ToggleElementVisible(GetInnerContainer(document));

            if (GetDocumentName(document) != "MainMenu")
            {
                GetInnerContainer(document).AddToClassList(USSAnimationClasses.Entering);
            }
        }
        GoToMainMenu();
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
                ToggleElementVisible(GetInnerContainer(OpenedUIDocument), false);
            }
            LookAtDocument(newDocument, false);
            ToggleElementVisible(GetInnerContainer(newDocument));
            OpenedUIDocument = newDocument;
        }
    }

    int GetUIDocumentIndex(UIDocument document)
    {
        return MainMenuUIDocuments.FindIndex(documentSearched => GetDocumentName(document) == GetDocumentName(documentSearched));
    }

    VisualElement GetInnerContainer(UIDocument mainMenuDocument)
    {
        return mainMenuDocument.rootVisualElement.Query<VisualElement>("Container");
    }

    void ToggleElementVisible(VisualElement element, bool isVisible = true)
    {
        element.AddToClassList(isVisible ? USSAnimationClasses.Flex : USSAnimationClasses.Hidden);
        element.RemoveFromClassList(isVisible ? USSAnimationClasses.Hidden : USSAnimationClasses.Flex);

    }

    string GetDocumentName(UIDocument uiDocument)
    {
        return uiDocument.ToString().Split(" ")[0];
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
        VisualElement newDocumentInnerContainer = GetInnerContainer(newDocument);
        VisualElement oldDocumentInnerContainer = GetInnerContainer(OpenedUIDocument);
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

        string enteringClass = !reverseTransition ? USSAnimationClasses.Entering : USSAnimationClasses.Exiting;
        string exitingClass = !reverseTransition ? USSAnimationClasses.Exiting : USSAnimationClasses.Entering;


        // Transition the new document in
        newDocumentInnerContainer.RemoveFromClassList(enteringClass);
        newDocumentInnerContainer.AddToClassList(USSAnimationClasses.ResetTranslation);
        newDocumentInnerContainer.AddToClassList(USSAnimationClasses.Flex);

        yield return null;

        // Transition the old document out
        if (OpenedUIDocument is not null)
        {
            oldDocumentInnerContainer.AddToClassList(exitingClass);
            yield return new WaitForSeconds(TransitionTimeMiliSeconds / 1000);
            oldDocumentInnerContainer.AddToClassList(USSAnimationClasses.Hidden);

        }

        newDocumentInnerContainer.RemoveFromClassList(USSAnimationClasses.ResetTranslation);


        OpenedUIDocument = newDocument;
        MainMenuTransitionInProgress = false;
        yield return null;
    }



}
