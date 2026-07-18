using UnityEngine;
using DG.Tweening;

public class BandMemberDialogue : DialogueManager
{
    //---References---//
    [Header("Starting Animation Refs")]
    [SerializeField] private RectTransform dialogueBorder;

    [Header("Intro Animation Config")]
    [SerializeField] private float duration = 0.25f;

    //---Init Methods---//
    protected override void OnEnable()
    {
        base.OnEnable(); // Runs parent's OnEnable if anything is there
    }

    protected override void OnDisable()
    {
        base.OnDisable(); // Runs parent's OnDisable
    }

    public void PrepareDialogue(TextAsset inkHomeDialogue)
    {
        if (inkHomeDialogue == null)
        {
            Debug.LogError("Missing inkHomeDialogue");
        }

        BeginDialogue();
    }

    // Override intro animation
    protected override void SetupStartingAnimation()
    {
        sequence = DOTween.Sequence();
        sequence.SetAutoKill(false);
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
            case "test":
                break;

            default:
                return;
        }
    }
}