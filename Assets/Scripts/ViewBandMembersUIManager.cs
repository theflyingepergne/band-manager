using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ViewBandMembersUIManager : Singleton<ViewBandMembersUIManager>
{
    [SerializeField] private GameObject bandMemberDetailsPanel;
    [SerializeField] private TextMeshProUGUI memberNameText;
    [SerializeField] private TextMeshProUGUI memberInstrumentText;
    [SerializeField] private TextMeshProUGUI memberGenreText;
    [SerializeField] private TextMeshProUGUI memberTraitsText;
    [SerializeField] private UIDocument talentLevelBar;

    private ProgressBar talentLevelProgressBar;

    private void Start()
    {
        // talentLevelProgressBar = talentLevelBar.rootVisualElement.Q<ProgressBar>("ProgressBar");
    }

    public void ShowBandMemberDetails(bool isActive, BandMemberData data = null)
    {
        // Set the details panel to active and populate it with the data from the BandMemberData
        bandMemberDetailsPanel.SetActive(isActive);
        memberNameText.text = "Looking at: " +data.memberName;
        memberInstrumentText.text = "Instruments: " + string.Join(", ", data.instruments);
        memberGenreText.text = "Genres: " + string.Join(", ", data.genres);
        memberTraitsText.text = "Traits: " + string.Join(", ", data.traits);

        // Set the talent level bar value based on the talent level in the data
        talentLevelProgressBar = talentLevelBar.rootVisualElement.Q<ProgressBar>("ProgressBar");
        if (talentLevelProgressBar != null)
        {
            talentLevelProgressBar.value = data.talentLevel;
            Debug.Log("Talent Level ProgressBar found and data source set.");
            Debug.Log($"UI Source Set! Member: {data.memberName}, Talent: {data.talentLevel}");
        }
        else
        {
            Debug.LogError("Talent Level ProgressBar not found in the UI Document.");
        }
        
    }
}
