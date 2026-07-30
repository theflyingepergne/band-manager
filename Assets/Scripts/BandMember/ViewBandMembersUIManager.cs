using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ViewBandMembersUIManager : MonoBehaviour
{
    //---References---//
    public static ViewBandMembersUIManager Instance { get; set; }

    [Header("Panel Right")]
    [SerializeField] private GameObject bandMemberDetailsPanel;
    [SerializeField] private GameObject detailsPanelRight;
    [SerializeField] private TMP_Text genresText;
    [SerializeField] private Image talentBarFillImage;
    [SerializeField] private TMP_Text traitsText;
    [SerializeField] private Button talkButton;
    [SerializeField] private Button closeButton;

    [Header("Panel Left")]
    [SerializeField] private GameObject detailsPanelLeft;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image sprite;
    [SerializeField] private RectTransform instrumentsPanel;

    [Header("Tooltip")]
    [SerializeField] private GameObject tooltipContainer;
    [SerializeField] private TMP_Text tooltipTitle;
    [SerializeField] private Image tooltipImage;
    [SerializeField] private TMP_Text tooltipDescription;

    [Header("Prefabs")]
    [SerializeField] private GameObject instrumentPanel;

    //---Local References---//
    private BandMemberInstance bandMemberInstance;
    private BandMemberDialogue bandMemberDialogue;

    //---Events---//
    void OnEnable()
    {
        ClickScript.OnClickEmptySpace += HandleClickEmptySpace;
        UIHoverInfo.OnHoverStarted += HandleLinkHovered;
        UIHoverInfo.OnHoverEnded += HandleLinkExited;
    }

    void OnDisable()
    {
        ClickScript.OnClickEmptySpace -= HandleClickEmptySpace;
        UIHoverInfo.OnHoverStarted -= HandleLinkHovered;
        UIHoverInfo.OnHoverEnded -= HandleLinkExited;
    }

    //---Init Methods---//
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Hide the details panel on start
        HideBandMemberDetails();
        bandMemberDialogue = FindAnyObjectByType<BandMemberDialogue>();
    }

    //---Input---//
    private void HandleClickEmptySpace()
    {
        HideBandMemberDetails();
    }

    //---Setup---//
    public void ShowBandMemberDetails(BandMemberInstance data = null)
    {
        // Populate panel with BandMemberInstance data
        if (data != null)
        {
            bandMemberInstance = data;

            nameText.text = bandMemberInstance.name;
            sprite.sprite = bandMemberInstance.sprite;
            genresText.text = string.Join(" ", bandMemberInstance.genreAffinities.FormatGenreAffinities(false));
            talentBarFillImage.fillAmount = bandMemberInstance.talentLevel / 10f;

            FormatTraitsText();

            PopulateInstrumentsPanel(bandMemberInstance);
        }

        bandMemberDetailsPanel.SetActive(true);
    }

    private void FormatTraitsText()
    {
        List<string> formattedTraitsText = new();

        for (int i = 0; i < bandMemberInstance.traits.Count; i++)
        {
            string t = $"<link=\"trait_{i}\">{bandMemberInstance.traits[i].traitName}</link>";
            formattedTraitsText.Add(t);
        }

        traitsText.text = string.Join(", ", formattedTraitsText);
    }

    private void PopulateInstrumentsPanel(BandMemberInstance data)
    {
        ClearInstrumentsPanel();

        foreach (var i in data.instruments)
        {
            GameObject newInstrumentPanel = Instantiate(instrumentPanel, instrumentsPanel, false);
            newInstrumentPanel
                .transform
                .GetChild(0)
                .GetComponent<Image>().sprite = i.sprite;
        }
    }

    private void ClearInstrumentsPanel()
    {
        for (int i = instrumentsPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(instrumentsPanel.GetChild(i).gameObject);
        }
    }

    public void HideBandMemberDetails()
    {
        bandMemberDetailsPanel.SetActive(false);
    }

    //---Dialogue---//
    public void Talk()
    {
        bandMemberDialogue.PrepareDialogue(bandMemberInstance.inkHomeDialogue, bandMemberInstance);
    }

    //---Tooltip---//
    private void HandleLinkHovered(string linkID)
    {
        PopulateTooltip(linkID);
    }

    private void PopulateTooltip(string linkID)
    {
        string[] parts = linkID.Split('_');

        string type = parts[0];
        int index = int.Parse(parts[1]);

        switch (type)
        {
            case "trait":
                DoTraitTooltip(index);
                break;
            case "instrument":
                DoInstrumentTooltip(index);
                break;
            default:
                return;
        }
    }

    private void DoTraitTooltip(int i)
    {
        var trait = bandMemberInstance.traits[i];

        tooltipTitle.text = trait.traitName;
        tooltipImage.sprite = trait.icon;
        tooltipDescription.text = trait.description;
        tooltipContainer.SetActive(true);
    }

    private void DoInstrumentTooltip(int i)
    {
        var instrument = bandMemberInstance.instruments[i];

        tooltipTitle.text = instrument.instrumentName;
        tooltipImage.sprite = instrument.sprite;
        tooltipDescription.text = instrument.description;
        tooltipContainer.SetActive(true);
    }

    private void HandleLinkExited()
    {
        ClearTooltip();
    }

    private void ClearTooltip()
    {
        tooltipContainer.SetActive(false);
    }
}
