using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewBandMembersUIManager : Singleton<ViewBandMembersUIManager>
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private GameObject BandMemberDetailsPanel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text genresText;
    [SerializeField] private Image talentBarFillImage;
    [SerializeField] private TMP_Text instrumentsText;
    [SerializeField] private TMP_Text traitsText;

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
            genresText.text = string.Join(", ", data.genreAffinities.GetGenresAsStrings());
            talentBarFillImage.fillAmount = data.talentLevel / 10f;
            instrumentsText.text = string.Join(", ", data.instruments.ConvertAll(i => i.instrumentName));
            // traitsText.text = string.Join(", ", data.traits);
        }

        BandMemberDetailsPanel.SetActive(true);
    }

    public void HideBandMemberDetails()
    {
        BandMemberDetailsPanel.SetActive(false);
    }
}
