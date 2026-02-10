using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ViewBandMemberUIManager : Singleton<ViewBandMemberUIManager>
{
    // [SerializeField] private GameObject bandMemberDetailsPanel;

    [SerializeField] private UIDocument ViewBandMemberUI;

    private VisualElement root;

    public void ShowBandMemberDetails(bool isActive, BandMemberData data = null)
    {
        // Set the details panel to active and populate it with the data from the BandMemberData
        if (data != null)
        {
            root = ViewBandMemberUI.rootVisualElement;
            root.dataSource = data; // Set the data source for the UI to the BandMemberData
            // root.Q<Label>("MemberNameLabel").text = data.memberName;
            // root.Q<Label>("MemberInstrumentsLabel").text =string.Join(", ", data.instruments);
            // root.Q<Label>("MemberGenresLabel").text = string.Join(", ", data.genres);
            // root.Q<Label>("MemberTraitsLabel").text = string.Join(", ", data.traits);
            // root.Q<ProgressBar>("TalentLevelProgressBar").value = data.talentLevel;
        }
    }

}
