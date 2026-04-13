using UnityEngine;
using UnityEngine.SceneManagement;

public class VenueIcon : MonoBehaviour, IClickable
{
    [SerializeField] private VenueData venueData;

    public void OnClicked()
    {
        // Debug.Log("Venue icon clicked: " + venueData.venueName);
        BandManager.Instance.SetVenue(venueData);
        SceneManager.LoadScene("Transit");
    }
}
