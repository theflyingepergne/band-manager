using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Collections.Generic;

public class ViewBandMembersUIManager : MonoBehaviour
{
    //---References---//
    public static ViewBandMembersUIManager Instance { get; set; }

    [Header("UI References")]
    [SerializeField] private GameObject bandMemberDetailsPanel;
    [SerializeField] private GameObject detailsPanelRight;
    [SerializeField] private GameObject detailsPanelLeft;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image sprite;
    [SerializeField] private TMP_Text genresText;
    [SerializeField] private Image talentBarFillImage;
    [SerializeField] private TMP_Text instrumentsText;
    [SerializeField] private TMP_Text traitsText;
    [SerializeField] private Button talkButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private RectTransform instrumentsPanel;

    [Header("Prefabs")]
    [SerializeField] private GameObject instrumentPanel;

    //---Local References---//
    private BandMemberInstance bandMemberInstance;
    private BandMemberDialogue bandMemberDialogue;

    //---Events---//
    void OnEnable()
    {
        ClickScript.OnClickEmptySpace += HandleClickEmptySpace;
        LinkTextInfo.OnLinkHovered += HandleLinkHovered;
        LinkTextInfo.OnLinkExited += HandleLinkExited;
    }

    void OnDisable()
    {
        ClickScript.OnClickEmptySpace -= HandleClickEmptySpace;
        LinkTextInfo.OnLinkHovered -= HandleLinkHovered;
        LinkTextInfo.OnLinkExited -= HandleLinkExited;
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
            instrumentsText.text = string.Join(", ", bandMemberInstance.instruments.ConvertAll(i => i.instrumentName));
            // traitsText.text = string.Join(", ", bandMemberInstance.traits.ConvertAll(t => t.traitName));

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
            string t = $"<link=\"{i}\">{bandMemberInstance.traits[i].traitName}</link>";
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

    public void Talk()
    {
        bandMemberDialogue.PrepareDialogue(bandMemberInstance.inkHomeDialogue, bandMemberInstance);
    }

    private void HandleLinkHovered(string linkID)
    {
        Debug.Log($"Hovering over {linkID}");
    }
    
    private void HandleLinkExited()
    {
        ClearTooltip();
        // Debug.Log("Stopped hovering");
    }

    private void ClearTooltip()
    {
        
    }
}
