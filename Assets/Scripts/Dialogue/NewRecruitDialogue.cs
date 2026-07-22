using UnityEngine;
using DG.Tweening;

public class NewRecruitDialogue : DialogueManager
{
    //---References---//
    [Header("New Recruit Refs")]
    [SerializeField] private GameObject newRecruit;
    [SerializeField] private RectTransform dialogueBorder;

    [Header("Intro Animation Config")]
    [SerializeField] private float duration = 0.25f;

    //---Local References---//
    NewRecruitManager newRecruitManager;

    //---Init Methods---//
    protected override void OnEnable()
    {
        base.OnEnable(); // Runs parent's OnEnable if anything is there
    }

    protected override void OnDisable()
    {
        base.OnDisable(); // Runs parent's OnDisable
    }

    public void PrepareDialogue(TextAsset newRecruitInk, Sprite newRecruitSprite, NewRecruitManager newRecruitManager)
    {
        if (newRecruitInk == null)
        {
            Debug.LogError("Missing newRecruitInk");
            return;
        }

        if (newRecruitManager == null)
        {
            Debug.LogError("Missing newRecruitManager");
            return;
        }

        inkJsonAsset = newRecruitInk;
        newRecruit.transform
            .GetChild(0)
            .GetComponent<SpriteRenderer>().sprite = newRecruitSprite;
        this.newRecruitManager = newRecruitManager;

        BeginDialogue();
        SetStoryVariables();
    }

    public void SetStoryVariables()
    {
        story.variablesState["recruitName"] = newRecruitManager.data.name;
    }

    // Override intro animation
    protected override void SetupStartingAnimation()
    {
        sequence = DOTween.Sequence();
        sequence.SetAutoKill(false);

        // Grab the MoveDialogueRecipient script off the newRecruit GameObject (or its child)
        if (newRecruit.TryGetComponent(out MoveDialogueRecipient mover))
        {
            // Append the character moving to the talking anchor
            sequence.Append(mover.MoveToTalkingAnchor(duration));
        }

        // Slide up the dialogue box border
        sequence.Append(dialogueBorder.DOAnchorPosY(50f, duration));

        sequence.Play().OnComplete(AdvanceDialogue);
    }

    // Override "Click to continue" input
    protected override bool IsDialoguePaused()
    {
        return false;
    }

    // Override unique event logic
    protected override void HandleDialogueEvent(string eventName, string eventParameter)
    {
        switch (eventName)
        {
            case "recruit":
                newRecruitManager.Recruit();
                break;

            default:
                return;
        }
    }
}