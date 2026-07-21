using UnityEngine;
using DG.Tweening;

public class BandMemberDialogue : DialogueManager
{
    //---References---//
    [Header("Starting Animation Refs")]
    [SerializeField] private RectTransform dialogueBorder;

    [Header("Intro Animation Config")]
    [SerializeField] private float duration = 0.25f;

    //---Local References---//
    private BandMemberInstance bandMemberInstance;

    //---Init Methods---//
    protected override void OnEnable()
    {
        base.OnEnable(); // Runs parent's OnEnable if anything is there
    }

    protected override void OnDisable()
    {
        base.OnDisable(); // Runs parent's OnDisable
    }

    public void PrepareDialogue(TextAsset inkHomeDialogue, BandMemberInstance talkingToBandMember)
    {
        if (inkHomeDialogue == null) Debug.LogError("Missing inkHomeDialogue");

        if (talkingToBandMember != null)
        {
            bandMemberInstance = talkingToBandMember;
        }

        BeginDialogue();
        SetStoryVariables();
    }

    public void SetStoryVariables()
    {
        story.variablesState["recruitName"] = bandMemberInstance.name;
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
            case "write_song":
                // set event data
                GameEventData writeSongEvent = ScheduleManager.Instance.scheduledEvents
                    .Find(e => e.eventID.Contains("Wrote A Song"))?
                    .gameEventData;
                
                // set date to be 1 - 3 days in the future
                GameDate futureDate = DateManager.Instance.date.AddDays(Random.Range(1, 3));

                // set event pop-up title
                string eventTitle = $"{bandMemberInstance.name} wrote a song! ";

                // set description

                ScheduleManager.Instance.ScheduleNewEvent
                (
                    writeSongEvent,
                    futureDate,
                    eventTitle
                );
                break;

            default:
                return;
        }
    }
}