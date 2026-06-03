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

            Debug.Log("I'm on the left hand side of the screen");
        }
        else
        {
            isOnLeftHandSide = false;
            venueInfoPanel.SetPanelPosition(isOnLeftHandSide);

            Debug.Log("I'm on the right hand side of the screen");

        }

        // OnClicked, activate UI and tell it what to show
        if (venueInfoPanel.gameObject.activeSelf == false)
        {
            venueInfoPanel.gameObject.SetActive(true);
        }
        venueInfoPanel.SetupVenueInfo(venueData); 
    }
}