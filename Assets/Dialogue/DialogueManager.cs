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

    [Header("UI")]
    [SerializeField] private RectTransform dialogueChoicesPanel;
    [SerializeField] private GameObject dialogueChoiceButtonPrefab;

    //---Local References---//
    private Story story;

    //---Methods---//
    private void Start()
    {
        story = new Story(inkJsonAsset.text);
        AdvanceDialogue();
    }

    public void AdvanceDialogue()
    {
        ClearChoiceButtons();

        if (story.canContinue)
        {
            dialogueText.text = story.Continue();
        }

        // Display any choices
        if (story.currentChoices.Count > 0)
        {
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
            // If there's no more story and no more choices, exit dialogue
            GameObject choiceButton = Instantiate(dialogueChoiceButtonPrefab, dialogueChoicesPanel, false);

            choiceButton.GetComponentInChildren<TMP_Text>().text = "[Leave]";

            choiceButton.GetComponent<Button>().onClick.AddListener(() => ExitDialogue());
        }
    }

    private void ExitDialogue()
    {
        gameObject.SetActive(false);
    }

    private void OnChoiceSelected(int index)
    {
        // Tell Ink which path the player picked
        story.ChooseChoiceIndex(index);

        // Move the story forward to show the choice response
        AdvanceDialogue();
    }

    private void ClearChoiceButtons()
    {
        int childCount = dialogueChoicesPanel.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            // dialogueChoicesPanel.transform.GetChild(i).transform.SetParent(null);
            Destroy(dialogueChoicesPanel.transform.GetChild(i).gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (story.canContinue)
        {
            AdvanceDialogue();
        }
    }
    // TODO: clicking advances dialogue
}