using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewBandMembersUIManager : Singleton<ViewBandMembersUIManager>
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private RectTransform BandMemberDetailsPanel;    
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
        ShowBandMemberDetails(false);
    }
    
    private void HandleClickEmptySpace()
    {
        ShowBandMemberDetails(false, null);
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
