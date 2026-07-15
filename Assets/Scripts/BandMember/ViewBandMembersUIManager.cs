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

    //---Events---//
    void OnEnable() => ClickScript.OnClickEmptySpace += HandleClickEmptySpace;
    void OnDisable() => ClickScript.OnClickEmptySpace -= HandleClickEmptySpace;

    //---Method---//
    void Start()
    {
        // Hide the details panel on start
        HideBandMemberDetails();
    }

    private void HandleClickEmptySpace()
    {
        HideBandMemberDetails();
    }

    public void ShowBandMemberDetails(BandMemberInstance data = null)
    {
        // Populate it with the data from the BandMemberData
        if (data != null)
        {            
            nameText.text = data.name;
            sprite.sprite = data.sprite;
            genresText.text = string.Join(", ", data.genreAffinities.GetGenresAsStrings());
            talentBarFillImage.fillAmount = data.talentLevel / 10f;
            instrumentsText.text = string.Join(", ", data.instruments.ConvertAll(i => i.instrumentName));
            traitsText.text = string.Join(", ", data.traits.ConvertAll(t => t.traitName));

            PopulateInstrumentsPanel(data);
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
                // .GetComponentInChildren<Image>().sprite = i.sprite;
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
}
