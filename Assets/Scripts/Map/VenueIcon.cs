using UnityEngine;

public class VenueIcon : MonoBehaviour, IClickable
{
    [Header("UI References")]
    // [SerializeField] private GameObject canvasVenueInfo;
    [SerializeField] private VenueInfoPanel venueInfoPanel;

    [Header("Data References")]
    [SerializeField] private VenueData venueData;

    public void OnClicked()
    {
        // // Debug.Log("Venue icon clicked: " + venueData.name);
        // BandManager.Instance.SetVenue(venueData);
        // SceneManager.LoadScene("Transit");

        // OnClicked, activate UI and tell it what to show
        if (venueInfoPanel.gameObject.activeSelf == false)
        {
            venueInfoPanel.gameObject.SetActive(true);
        }

        venueInfoPanel.SetupVenueInfo(venueData);
    }
}
