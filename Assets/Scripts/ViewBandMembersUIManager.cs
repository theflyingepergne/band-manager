using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewBandMemberUIManager : Singleton<ViewBandMemberUIManager>
{
    [SerializeField] private RectTransform BandMemberDetailsPanel;
    
    [Header("Band Member Details")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text genresText;
    [SerializeField] private Image talentBarFillImage;
    [SerializeField] private TMP_Text instrumentsText;
    [SerializeField] private TMP_Text traitsText;

    void Start()
    {
        // Hide the details panel on start
        ShowBandMemberDetails(false);
    }

    public void ShowBandMemberDetails(bool isActive = false, BandMemberData data = null)
    {
        // Populate it with the data from the BandMemberData
        if (data != null)
        {
            nameText.text = data.memberName;
            genresText.text = string.Join(", ", data.genres.ConvertAll(g => g.genreName));
            talentBarFillImage.fillAmount = data.talentLevel / 10f;
            instrumentsText.text = string.Join(", ", data.instruments.ConvertAll(i => i.instrumentName));
            traitsText.text = string.Join(", ", data.traits);
        }

        // Toggle detais panel
        if (isActive)
        {
            BandMemberDetailsPanel.gameObject.SetActive(true);
        }
        else
        {
            BandMemberDetailsPanel.gameObject.SetActive(false);
        }

    }
}
