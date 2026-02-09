using TMPro;
using UnityEngine;

public class ViewBandMembersUIManager : Singleton<ViewBandMembersUIManager>
{
    [SerializeField] private GameObject bandMemberDetailsPanel;
    [SerializeField] private TextMeshProUGUI memberNameText;

    public void ShowBandMemberDetails(bool isActive, BandMemberData data = null)
    {
        // Set the details panel to active and populate it with the data from the BandMemberData
        bandMemberDetailsPanel.SetActive(isActive);
        memberNameText.text = data.memberName;
    }
}
