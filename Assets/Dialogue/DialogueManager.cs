using DG.Tweening;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DialogueManager : Singleton<DialogueManager>, IPointerClickHandler
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
    private List<string> trackedVariables = new();
    private GameDate chosenDate;
    private Story story;

    //---Events---//
    public static System.Action<string, string> OnDialogueTagEncountered;

    private void OnEnable()
    {
        CalendarDay.OnInspectDay += HandleInspectDay;
    }
    private void OnDisable()
    {
        CalendarDay.OnInspectDay -= HandleInspectDay;
        RemoveVariableTracking();
    }


    //---Methods---//
    public void BeginDialogue()
    {
        story = new Story(inkJsonAsset.text);

        AddVariableTracking();
        dialogueText.text = "";

        // Initialize sequence, AdvanceDialogue when it's finished
        Sequence sequence = DOTween.Sequence().OnComplete(AdvanceDialogue);
        sequence.Append(venueOwner.transform.DOMoveX(4.96f, duration));
        sequence.Append(dialogueBorder.DOAnchorPosY(50f, duration));
        sequence.Play();
    }

    public void AdvanceDialogue()
    {
        ClearChoiceButtons();

        if (story.canContinue)
        {
            dialogueText.text = story.Continue();
            continueText.SetActive(true);

            HandleLineTags(story.currentTags);
        }

        // Display any choices
        if (story.currentChoices.Count > 0)
        {
            continueText.SetActive(false);

            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                GameObject choiceButton = Instantiate(dialogueChoiceButtonPrefab, dialogueChoicesPanel, false);

                choiceButton.GetComponentInChildren<TMP_Text>().text = choice.text;

                int choiceIndex = i;
                choiceButton.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choiceIndex));
            }
        }
        else if (!story.canContinue)
        {
            continueText.SetActive(false);

            // If there's no more story and no more choices, exit dialogue
            GameObject choiceButton = Instantiate(dialogueChoiceButtonPrefab, dialogueChoicesPanel, false);

            choiceButton.GetComponentInChildren<TMP_Text>().text = "[Leave]";

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

    public void OnPointerClick(PointerEventData eventData)
    {
        // Click to continue story
        if (story.canContinue)
        {
            AdvanceDialogue();
        }
    }

    private void HandleLineTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            // If tag contains a colon, second tag is the parameter
            if (tag.Contains(":"))
            {
                // Split the string at the colon into an array of strings
                // "PlaySound:Slide" becomes ["PlaySound", "Slide"]
                string[] splitTag = tag.Split(':');

                string type = splitTag[0];       // e.g "PlaySound"
                string parameter = splitTag[1];  // e.g "Boing" sound name

                // Broadcast it to the rest of your game!
                OnDialogueTagEncountered?.Invoke(type, parameter);
            }
            else
            {
                // If there's no colon, pass the whole tag & leave the parameter blank
                OnDialogueTagEncountered?.Invoke(tag, "");
            }
        }
    }

    private void HandleInspectDay(CalendarDay calendarDay, List<ScheduledEvent> events)
    {
        chosenDate = calendarDay.localDate;
        story.variablesState["chosen_date"] = chosenDate.GetDateAsString();
    }

    //---Listening for ink variable changes---//
    private void AddVariableTracking()
    {
        story.ObserveVariable("chosen_date", OnDateChosen);
    }

    private void RemoveVariableTracking()
    {
        story.RemoveVariableObserver(OnDateChosen);
    }

    private void OnDateChosen(string varName, object value)
    {
        var (anyGigs, gig) = ScheduleManager.Instance.CheckAnyGigsOnDay(chosenDate);
        if (anyGigs)
        {
            // can't book gig if there are already gigs scheduled on chosenDate
            string declineReason = "<i>The owner sighs.</i>\\n'You're already playing a gig on that day.'";
            story.variablesState["decline_reason"] = declineReason;
            return;
        }
        else if (chosenDate.IsBeforeDate(DateManager.Instance.date))
        {
            // can't book gig if the date we've chosen is in the past
            string declineReason = "<i>The owner sighs.</i>\\n'Obviously not, that date is in the past. Thanks for wasting my time by the way.'";
            story.variablesState["decline_reason"] = declineReason;
            return;
        }
        else if (BandManager.Instance.destinationVenue == null)
        {
            // can't book gig if we haven't chosen a venue
            string declineReason = "<i>The owner sighs.</i>\\n'You haven't chosen a venue... How did you even do that?'";
            story.variablesState["decline_reason"] = declineReason;
            return;
        }
        else
        {
            // if all other checks are false, we can book the gig
            story.variablesState["should_accept_booking"] = true;
        }

    }
}