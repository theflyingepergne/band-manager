using DG.Tweening;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using NUnit.Framework;

public class DialogueManager : Singleton<DialogueManager>
{
    //---References---//
    [Header("Core Dialogue")]
    [SerializeField] private TextAsset inkJsonAsset;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject continueText;

    [Header("UI")]
    [SerializeField] private RectTransform dialogueChoicesPanel;
    [SerializeField] private GameObject dialogueChoiceButtonPrefab;

    [Header("Animation")]
    [SerializeField] private GameObject venueOwner;
    [SerializeField] private RectTransform dialogueBorder;
    [SerializeField] private float duration = 1f;

    //---Local References---//
    private GameDate chosenDate;
    private Story story;
    private TypewriterEffect typewriter;
    public bool isBookingGig = false;
    private bool HasChoices => dialogueChoicesPanel.childCount > 0;


    //---Events---//
    public static System.Action<string, string> OnDialogueTagEncountered;

    private void OnEnable()
    {
        CalendarDay.OnInspectDay += HandleInspectDay;
        CalendarManager.OnChangeCalendarVisibility += HandleCalendarVisibilityState;
        TypewriterEffect.CompleteTextRevealed += HandleCompleteTextRevealed;
    }
    private void OnDisable()
    {
        CalendarDay.OnInspectDay -= HandleInspectDay;
        CalendarManager.OnChangeCalendarVisibility -= HandleCalendarVisibilityState;
        TypewriterEffect.CompleteTextRevealed -= HandleCompleteTextRevealed;
        if (story != null) story.UnbindExternalFunction("check_booking_status");
    }

    //---Methods---//
    public void BeginDialogue()
    {
        // init story and TypewriterEffect.cs
        story = new Story(inkJsonAsset.text);

        /// listen for ink external function
        story.BindExternalFunction("check_booking_status", () =>
        {
            EvaluateBookingGig();
        });

        typewriter = dialogueText.GetComponent<TypewriterEffect>();

        // clear text box
        dialogueText.text = "";

        // setup (and play) animation
        SetupStartingAnimation();
    }

    private void SetupStartingAnimation()
    {
        // Initialize sequence
        Sequence sequence = DOTween.Sequence();
        sequence.Append(venueOwner.transform.DOMoveX(4.96f, duration));
        sequence.Append(dialogueBorder.DOAnchorPosY(50f, duration));

        // when sequence is finished playing, AdvanceDialogue
        sequence.Play().OnComplete(AdvanceDialogue);
    }

    public void AdvanceDialogue()
    {
        // clear any choice buttons for safety
        ClearChoiceButtons();

        // hide "Click to ontinue" while text is being typewritten
        DisplayContinueText(false);

        // if there are more lines to the story, process next line
        if (story.canContinue)
        {
            // store ref to next line of story
            string nextLine = story.Continue();

            // if we've attached a TypewriterEffect.cs, startTypewriter
            if (typewriter != null)
            {
                // tell typewriter what to type
                typewriter.StartTypewriter(nextLine);
            }

            // if there's no TypewriterEffect.cs, continue story
            else
            {
                // set text box text to next line of story
                dialogueText.text = nextLine;
                HandleCompleteTextRevealed();
            }
        }

        // if there is no more text but choices are waiting
        else if (story.currentChoices.Count > 0)
        {
            HandleCompleteTextRevealed();
        }
    }

    private void Update()
    {
        // if there's no story (like when playing a gig) do nothing
        if (story == null) return;

        // Check for the single left-click input here
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 1. Guard: If the player is currently interacting with the calendar, freeze everything
            if (isBookingGig) return;
            if (HasChoices) return;

            if (typewriter != null)
            {
                // 2. If it's typing, the click should pass down to the typewriter to speed up/skip
                if (typewriter.isTyping)
                {
                    // This replaces the internal Update click detection that was in TypewriterEffect
                    if (!typewriter.currentlySkipping)
                    {
                        // Call a public method or handle via an explicit reference change.
                        // We can invoke it directly since it's public.
                        typewriter.Skip(true);
                        // Note: Since Skip(bool doSkip) is private/protected in your code, 
                        // make sure you change 'private void Skip(bool doSkip)' to 'public void Skip(bool doSkip)' in TypewriterEffect.cs!
                    }
                    return;
                }

                // 3. If it's NOT typing, the click advances the story line
                AdvanceDialogue();
            }
            else
            {
                // Fallback if there is no typewriter component
                AdvanceDialogue();
            }
        }
    }

    private void HandleCompleteTextRevealed()
    {
        // show "Click to continue" text
        DisplayContinueText(true);

        // handle any line tags
        ProcessLineTags(story.currentTags);

        // clear any choice buttons
        ClearChoiceButtons();

        // display any choices
        DisplayAnyChoices();
    }

    private void DisplayContinueText(bool visible)
    {
        // "Click to Continue" text
        continueText.SetActive(visible);
    }

    private void DisplayAnyChoices()
    {
        // If there are any choices...
        if (story.currentChoices.Count > 0)
        {
            // hide "Click to Continue" text
            DisplayContinueText(false);

            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                // Create choice buttons for how many choices there are
                Choice choice = story.currentChoices[i];
                GameObject choiceButton = Instantiate(dialogueChoiceButtonPrefab, dialogueChoicesPanel, false);

                choiceButton.GetComponentInChildren<TMP_Text>().text = choice.text;

                // Tell story which choice we chose
                int choiceIndex = i;
                choiceButton.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choiceIndex));
            }
        }

        // If there's no more story and no more choices, show [End Conversation] button
        else if (!story.canContinue)
        {
            DisplayContinueText(false);

            GameObject choiceButton = Instantiate(dialogueChoiceButtonPrefab, dialogueChoicesPanel, false);

            choiceButton.GetComponentInChildren<TMP_Text>().text = "[End Conversation]";
            choiceButton.GetComponent<Button>().onClick.AddListener(() => EndDialogue());
        }
    }

    private void EndDialogue()
    {
        gameObject.SetActive(false);
    }

    private void OnChoiceSelected(int index)
    {
        story.ChooseChoiceIndex(index);

        // Move the story forward to show the choice response
        AdvanceDialogue();
    }

    private void ClearChoiceButtons()
    {
        int childCount = dialogueChoicesPanel.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(dialogueChoicesPanel.transform.GetChild(i).gameObject);
        }
    }

    private void ProcessLineTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            // If tag contains a colon, second tag is the parameter
            if (tag.Contains(":"))
            {
                // Split the string at the colon into an array of strings
                // e.g "PlaySound:Boing" becomes ["PlaySound", "Boing"]
                string[] splitTag = tag.Split(':');

                string type = splitTag[0];       // e.g "PlaySound"
                string parameter = splitTag[1];  // e.g "Boing" sound name

                // Broadcast it to any subscribers e.g AudioManager
                OnDialogueTagEncountered?.Invoke(type, parameter);
            }
            else
            {
                // If there's no colon, pass the whole tag & leave the parameter blank
                OnDialogueTagEncountered?.Invoke(tag, "");
            }
        }
    }

    //---Booking Gigs---//
    private void HandleCalendarVisibilityState(bool visible)
    {
        isBookingGig = visible;
    }

    private void HandleInspectDay(CalendarDay calendarDay, List<ScheduledEvent> events)
    {
        chosenDate = calendarDay.localDate;
        story.variablesState["chosen_date"] = chosenDate.GetDateAsString();
    }

    private void EvaluateBookingGig()
    {
        var (anyGigs, gig) = ScheduleManager.Instance.CheckAnyGigsOnDay(chosenDate);
        if (anyGigs)
        {
            story.variablesState["decline_reason"] = "<style=desc>The owner sighs.</style>\n<style=speech>You're already playing a gig on that day.";
            story.variablesState["should_accept_booking"] = false;
        }
        else if (chosenDate.IsBeforeDate(DateManager.Instance.date))
        {
            story.variablesState["decline_reason"] = "<style=desc>The owner sighs.</style>\n<style=speech>Obviously not, that date is in the past.";
            story.variablesState["should_accept_booking"] = false;
        }
        else if (BandManager.Instance.destinationVenue == null)
        {
            story.variablesState["decline_reason"] = "<style=desc>The owner sighs.</style>\n<style=speech>You haven't chosen a venue... How did you even do that?";
            story.variablesState["should_accept_booking"] = false;
        }
        else
        {
            story.variablesState["should_accept_booking"] = true;
        }
    }
}