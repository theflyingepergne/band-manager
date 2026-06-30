using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class VenueOwnerDialogue : DialogueManager
{
    [Header("Venue Owner Refs")]
    [SerializeField] private GameObject venueOwner;
    [SerializeField] private RectTransform dialogueBorder;

    [Header("Intro Animation Config")]
    [SerializeField] private float duration = 1f;

    private GameDate chosenDate;
    private bool isBookingGig = false;

    protected override void OnEnable()
    {
        base.OnEnable(); // Runs parent's OnEnable if anything is there

        CalendarDay.OnInspectDay += HandleInspectDay;
        CalendarManager.OnChangeCalendarVisibility += HandleCalendarVisibilityState;
    }

    protected override void OnDisable()
    {
        base.OnDisable(); // Runs parent's OnDisable

        CalendarDay.OnInspectDay -= HandleInspectDay;
        CalendarManager.OnChangeCalendarVisibility -= HandleCalendarVisibilityState;
    }

    // Overriding the intro animation to do the DOTween sequence
    protected override void SetupStartingAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(venueOwner.transform.DOMoveX(4.96f, duration));
        sequence.Append(dialogueBorder.DOAnchorPosY(50f, duration));

        sequence.Play().OnComplete(AdvanceDialogue);
    }

    // Overriding the safety guard for dialogue inputs
    protected override bool IsDialoguePaused()
    {
        return isBookingGig;
    }

    protected override void HandleDialogueEvent(string eventName, string eventParameter)
    {
        switch (eventName)
        {            
            case "check_booking":
                EvaluateBookingGig();
                break;

            default:
                return;
        }
    }

    //---Booking Gigs Logic (Completely isolated here)---//
    private void HandleCalendarVisibilityState(bool visible)
    {
        isBookingGig = visible;
        if (!isBookingGig) AdvanceDialogue();
    }

    private void HandleInspectDay(CalendarDay calendarDay, List<ScheduledEvent> events)
    {
        chosenDate = calendarDay.localDate;
        story.variablesState["chosen_date"] = chosenDate.GetDateAsString(); // Accessible because 'story' is protected
    }

    private void EvaluateBookingGig()
    {
        var (anyGigs, gig) = ScheduleManager.Instance.CheckAnyGigsOnDay(chosenDate);

        if (chosenDate.IsSameDate(new GameDate(0, 0, 0)))
        {
            story.variablesState["decline_reason"] = "<style=desc>The owner sighs.</style>\nYeah, so you have to actually choose a date if you want to book a gig.";
            story.variablesState["should_accept_booking"] = false;
        }
        else if (anyGigs)
        {
            story.variablesState["decline_reason"] = "<style=desc>The owner sighs.</style>\nYou're already playing a gig on that day.";
            story.variablesState["should_accept_booking"] = false;
        }
        else if (chosenDate.IsBeforeDate(DateManager.Instance.date))
        {
            story.variablesState["decline_reason"] = "<style=desc>The owner sighs.</style>\nObviously not, that date is in the past.";
            story.variablesState["should_accept_booking"] = false;
        }
        else if (BandManager.Instance.destinationVenue == null)
        {
            story.variablesState["decline_reason"] = "<style=desc>The owner sighs.</style>\nYou haven't chosen a venue... How did you even do that?";
            story.variablesState["should_accept_booking"] = false;
        }
        else
        {
            story.variablesState["should_accept_booking"] = true;
        }
    }
}