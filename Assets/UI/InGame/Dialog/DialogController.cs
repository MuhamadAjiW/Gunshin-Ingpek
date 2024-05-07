using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogController : InGameUIScreenController
{
    public static DialogController Instance;

    // Attributes
    private Dialog dialogOverlay;
    private CutsceneData currentCutscene;
    private int cutsceneProgress;

    // for pseudo animation
    private float animationSpeed = 10f;
    private float initialY = 0f;
    private float endY = -50f;
    private Coroutine currentAnimationLeft;
    private Coroutine currentAnimationRight;

    // Events
    public event Action OnCutsceneFinished;

    // Constructor
    public new void OnEnable()
    {
        base.OnEnable();
        Instance = this;
        dialogOverlay = rootElement.Q<Dialog>();

        initialY = dialogOverlay.m_PersonLImage.style.top.value.value;
        endY += initialY;
    }

    public void StartCutscene(CutsceneData cutsceneData)
    {
        GameController.stateController.PushState(GameState.CUTSCENE);

        currentCutscene = cutsceneData;
        cutsceneProgress = -1;
        ProgressCutscene();
    }

    public void ProgressCutscene()
    {
        StopAnimation();
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
        dialogOverlay.m_Overlay.style.backgroundImage = new StyleBackground(data.OverlayBackground);

        dialogOverlay.m_PersonLLabelContainer.visible = data.PersonLActive;
        dialogOverlay.m_PersonLImage.visible = data.PersonLActive;
        dialogOverlay.m_PersonLImage.style.backgroundImage = new StyleBackground(data.PersonLImage);

        dialogOverlay.m_PersonLLabelText.text = data.PersonLName;

        dialogOverlay.m_PersonRLabelContainer.visible = data.PersonRActive;
        dialogOverlay.m_PersonRImage.visible = data.PersonRActive;
        dialogOverlay.m_PersonRImage.style.backgroundImage = new StyleBackground(data.PersonRImage);
        dialogOverlay.m_PersonRLabelText.text = data.PersonRName;
        
        dialogOverlay.m_MainText.text = data.Text;

        Animate(data.animation);
    }

    public void StopAnimation()
    {
        if (currentAnimationLeft != null)
        {
            StopCoroutine(currentAnimationLeft);
            dialogOverlay.m_PersonLImage.style.top = initialY;
        }
        if (currentAnimationRight != null)
        {
            StopCoroutine(currentAnimationRight);
            dialogOverlay.m_PersonRImage.style.top = initialY;
        }
    }

    public void Animate(DialogAnimation code)
    {
        // StopAnimation();
        switch (code)
        {
            case DialogAnimation.ANIMATE_NONE:
                return;
            case DialogAnimation.ANIMATE_LEFT:
                currentAnimationLeft = StartCoroutine(AnimatePersonCoroutine(DialogAnimation.ANIMATE_LEFT));
                return;
            case DialogAnimation.ANIMATE_RIGHT:
                currentAnimationRight = StartCoroutine(AnimatePersonCoroutine(DialogAnimation.ANIMATE_RIGHT));
                return;
            case DialogAnimation.ANIMATE_BOTH:
                currentAnimationLeft = StartCoroutine(AnimatePersonCoroutine(DialogAnimation.ANIMATE_LEFT));
                currentAnimationRight = StartCoroutine(AnimatePersonCoroutine(DialogAnimation.ANIMATE_RIGHT));
                return;
        }
    }

    private IEnumerator AnimatePersonCoroutine(DialogAnimation code)
    {
        yield return new WaitForSecondsRealtime(0.005f);
        float targetY = endY;
        float newY = initialY;
        switch (code)
        {
            case DialogAnimation.ANIMATE_LEFT:
                while (Mathf.Abs(newY - targetY) > 5f)
                {
                    newY = Mathf.Lerp(newY, targetY, 0.05f);
                    dialogOverlay.m_PersonLImage.style.top = newY;

                    yield return new WaitForSecondsRealtime(0.001f);
                }
                dialogOverlay.m_PersonLImage.style.top = targetY;
                
                targetY = initialY;

                newY = endY;
                while (Mathf.Abs(newY - targetY) > 5f)
                {
                    newY = Mathf.Lerp(newY, targetY, 0.05f);
                    dialogOverlay.m_PersonLImage.style.top = newY;

                    yield return new WaitForSecondsRealtime(0.001f);
                }
                dialogOverlay.m_PersonLImage.style.top = targetY;
                break;

            case DialogAnimation.ANIMATE_RIGHT:
                while (Mathf.Abs(newY - targetY) > 5f)
                {
                    newY = Mathf.Lerp(newY, targetY, 0.05f);
                    dialogOverlay.m_PersonRImage.style.top = newY;

                    yield return new WaitForSecondsRealtime(0.001f);
                }
                dialogOverlay.m_PersonRImage.style.top = targetY;
                
                targetY = initialY;

                newY = endY;
                while (Mathf.Abs(newY - targetY) > 5f)
                {
                    newY = Mathf.Lerp(newY, targetY, 0.05f);
                    dialogOverlay.m_PersonRImage.style.top = newY;

                    yield return new WaitForSecondsRealtime(0.001f);
                }
                dialogOverlay.m_PersonRImage.style.top = targetY;
                break;
            
            default:
                yield return null;
                break;
        }
        
    }
}
