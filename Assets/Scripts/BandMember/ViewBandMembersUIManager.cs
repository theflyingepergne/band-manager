using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewBandMembersUIManager : Singleton<ViewBandMembersUIManager>
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private GameObject BandMemberDetailsPanel;
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
    void OnEnable() => ClickScript.OnClickEmptySpace += HandleClickEmptySpace;
    void OnDisable() => ClickScript.OnClickEmptySpace -= HandleClickEmptySpace;

    //---Init Methods---//
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
            genresText.text = string.Join(", ", bandMemberInstance.genreAffinities.GetGenresAsStrings());
            talentBarFillImage.fillAmount = bandMemberInstance.talentLevel / 10f;
            instrumentsText.text = string.Join(", ", bandMemberInstance.instruments.ConvertAll(i => i.instrumentName));
            traitsText.text = string.Join(", ", bandMemberInstance.traits.ConvertAll(t => t.traitName));

            PopulateInstrumentsPanel(bandMemberInstance);
        }

        BandMemberDetailsPanel.SetActive(true);
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
        BandMemberDetailsPanel.SetActive(false);
    }

    public void Talk()
    {
        bandMemberDialogue.PrepareDialogue(bandMemberInstance.inkHomeDialogue);
    }
}
