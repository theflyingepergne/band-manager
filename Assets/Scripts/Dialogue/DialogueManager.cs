using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

// Keep core dialogue loop mechanics here
public class DialogueManager : Singleton<DialogueManager>
{
    //---References (Changed to protected so child classes can see them)---//
    [Header("Core Dialogue")]
    [SerializeField] protected TextAsset inkJsonAsset;
    [SerializeField] protected TMP_Text dialogueText;
    [SerializeField] protected GameObject continueText;

    [Header("UI")]
    [SerializeField] protected RectTransform dialogueChoicesPanel;
    [SerializeField] protected GameObject dialogueChoiceButtonPrefab;

    //---Local References---//
    protected Story story;
    private TypewriterEffect typewriter;
    private bool HasChoices => dialogueChoicesPanel.childCount > 0;
    private bool isProcessingQueuedEvents;

    //---Events---//
    public static System.Action<string, string> OnDialogueTagEncountered;
    public static System.Action<string, string> OnDialogueEventTriggered;
    // public static System.Action<string, string> OnAfterTypingDialogueEvent;
    private readonly List<System.Action> pendingDialogueEvents = new();

    //---Init Methods---//
    protected virtual void OnEnable(){}

    protected virtual void OnDisable(){}

    public virtual void BeginDialogue()
    {
        story = new Story(inkJsonAsset.text);
        story.BindExternalFunction("trigger_dialogue_event", (string eventName, string eventParameter) =>
        {
            EvaluateDialogueEvent(eventName, eventParameter);
        });

        typewriter = dialogueText.GetComponent<TypewriterEffect>();
        typewriter.CompleteTextRevealed += HandleCompleteTextRevealed;

        dialogueText.text = "";

        SetupStartingAnimation();
    }

    protected virtual void EndDialogue()
    {
        typewriter.CompleteTextRevealed -= HandleCompleteTextRevealed;
        OnDialogueEventTriggered -= EvaluateDialogueEvent;
        story.UnbindExternalFunction("trigger_dialogue_event");
        gameObject.SetActive(false);
    }

    // Marked virtual so different NPC types can do different intro transitions
    protected virtual void SetupStartingAnimation()
    {
        AdvanceDialogue();
    }

    //---Dialogue Loop---//
    public void AdvanceDialogue()
    {
        ClearChoiceButtons();
        DisplayContinueText(false);

        if (story.canContinue)
        {
            string nextLine = story.Continue();
            ProcessLineTags(story.currentTags);

            if (typewriter != null) typewriter.StartTypewriter(nextLine);
            else
            {
                dialogueText.text = nextLine;
                HandleCompleteTextRevealed();
            }
        }
        else if (story.currentChoices.Count > 0)
        {
            HandleCompleteTextRevealed();
        }
    }

    protected virtual void Update()
    {
        if (story == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (HasChoices) return;
            if (IsDialoguePaused()) return; // Virtual check for child classes to pause input

            if (typewriter != null && typewriter.IsTyping)
            {
                typewriter.Skip(true);
                return;
            }
            AdvanceDialogue();
        }
    }

    // A hook for child classes to freeze input
    protected virtual bool IsDialoguePaused()
    {
        return false;
    }

    private void HandleCompleteTextRevealed()
    {
        DisplayContinueText(true);
        FlushQueuedDialogueEvents();
        ClearChoiceButtons();
        DisplayAnyChoices();
    }

    private void DisplayContinueText(bool visible)
    {
        // "Click to Continue" text
        continueText.SetActive(visible);
    }

    //---Dialogue Choices---//
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

    private void OnChoiceSelected(int index)
    {
        story.ChooseChoiceIndex(index);

        // Move the story forward to show the choice response
        AdvanceDialogue();
    }

    private void ClearChoiceButtons()
    {
        int childCount = dialogueChoicesPanel.transform.childCount;

        // destroy each child starting from the end
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(dialogueChoicesPanel.transform.GetChild(i).gameObject);
        }
    }

    //---Dialogue Tags---//
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

    //---Dialogue Events---//
    protected void EvaluateDialogueEvent(string eventName, string eventParameter)
    {
        if (eventParameter == "after_typing" && !isProcessingQueuedEvents)
        {
            // If event specifically asks to wait, and we aren't currently flushing the queue,
            // Queue a lambda that will re-run this method, but mark it to fire immediately next time
            pendingDialogueEvents.Add(() => EvaluateDialogueEvent(eventName, ""));
        }
        else
        {
            // Tell child to run custom logic
            HandleDialogueEvent(eventName, eventParameter);

            // Broadcast event to subscribers
            OnDialogueEventTriggered?.Invoke(eventName, eventParameter);
        }
    }

    protected virtual void HandleDialogueEvent(string eventName, string eventParameter)
    {
        // Override logic in child classes
    }

    private void FlushQueuedDialogueEvents()
    {
        isProcessingQueuedEvents = true;

        foreach (var dialogueEvent in pendingDialogueEvents)
        {
            dialogueEvent?.Invoke();
        }

        pendingDialogueEvents.Clear();
        isProcessingQueuedEvents = false;
    }

    protected virtual void QueueDialogueEvent(string eventName, string eventParameter)
    {
        Debug.Log("Queued event " + eventName);
        pendingDialogueEvents.Add(() => OnDialogueEventTriggered?.Invoke(eventName, eventParameter));
    }
}