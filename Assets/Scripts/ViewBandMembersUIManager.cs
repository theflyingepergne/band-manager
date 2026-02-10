using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ViewBandMemberUIManager : Singleton<ViewBandMemberUIManager>
{
    [SerializeField] private UIDocument ViewBandMemberUI;

    private VisualElement root;
    private VisualElement detailsWrapper;

    void Start()
    {
        root = ViewBandMemberUI.rootVisualElement;
        detailsWrapper = root.Q<VisualElement>("DetailsWrapper");
    }

    public void ShowBandMemberDetails(bool isActive, BandMemberData data = null)
    {
        // Set the details panel to active and populate it with the data from the BandMemberData
        if (data != null)
        {
            root.dataSource = data; // Set the data source for the UI to the BandMemberData
        }

        if (isActive)
        {
            detailsWrapper.RemoveFromClassList("hide");
        }
        else
        {
            detailsWrapper.AddToClassList("hide");
        }

    }
}
