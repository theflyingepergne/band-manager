using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ViewBandMemberUIManager : Singleton<ViewBandMemberUIManager>
{
    // get reference to the BandMemberDetailsPanel which is a child of the UI Canvas, not using uitoolkit
    [SerializeField] private RectTransform BandMemberDetailsPanel;
    [Header("Band Member Details")]
    [SerializeField] private TMP_Text nameText;

    void Start()
    {
        // Hide the details panel on start
        ShowBandMemberDetails(false);
    }

    public void ShowBandMemberDetails(bool isActive = false, BandMemberData data = null)
    {
        // Set the details panel to active and populate it with the data from the BandMemberData
        if (data != null)
        {
            nameText.text = data.memberName;
        }

        if (isActive)
        {
            // detailsWrapper.RemoveFromClassList("hide");
            BandMemberDetailsPanel.gameObject.SetActive(true);
        }
        else
        {
            // detailsWrapper.AddToClassList("hide");
            BandMemberDetailsPanel.gameObject.SetActive(false);
        }

    }
}
