using UnityEngine;

public class ViewBandMemberUIManager : MonoBehaviour
{
    // get reference to the BandMemberDetailsPanel which is a child of the UI Canvas, not using uitoolkit
    [SerializeField] private RectTransform BandMemberDetailsPanel;


    void Start()
    {
        // Hide the details panel at the start
        ShowBandMemberDetails(false);
    }

    public void ShowBandMemberDetails(bool isActive = false, BandMemberData data = null)
    {
        // Set the details panel to active and populate it with the data from the BandMemberData
        // if (data != null)
        // {
        //     // root.dataSource = data; // Set the data source for the UI to the BandMemberData
        // }

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
