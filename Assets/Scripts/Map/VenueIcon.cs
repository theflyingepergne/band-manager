using UnityEngine;

public class VenueIcon : MonoBehaviour, IClickable
{
    [Header("UI References")]
    [SerializeField] private VenueInfoPanel venueInfoPanel;

    [Header("Data References")]
    [SerializeField] private VenueData venueData;

    public void OnClicked()
    {
        bool isOnLeftHandSide;

        if (gameObject.transform.position.x < 0)
        {
            isOnLeftHandSide = true;
            venueInfoPanel.SetPanelPosition(isOnLeftHandSide);
        }
        else
        {
            isOnLeftHandSide = false;
            venueInfoPanel.SetPanelPosition(isOnLeftHandSide);
        }

        // OnClicked, activate UI and tell it what to show
        if (venueInfoPanel.gameObject.activeSelf == false)
        {
            venueInfoPanel.gameObject.SetActive(true);
        }
        venueInfoPanel.SetupVenueInfo(venueData); 
    }
}