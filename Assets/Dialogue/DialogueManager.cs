using DG.Tweening;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IPointerClickHandler
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
    private Story story;

    //---Methods---//
    public void BeginDialogue()
    {
        gameObject.SetActive(true);
        story = new Story(inkJsonAsset.text);

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
}