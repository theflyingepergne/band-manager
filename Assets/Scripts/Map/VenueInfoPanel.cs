using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class VenueInfoPanel : MonoBehaviour
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private TMP_Text venueName;
    [SerializeField] private Image venueSprite;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text distance;
    [SerializeField] private TMP_Text capacity;
    [SerializeField] private TMP_Text bookingFee;

    private VenueData currentVenue;

    //---Methods---//
    public void SetupVenueInfo(VenueData venue)
    {
        currentVenue = venue;
        venueName.text = venue.name;
        venueSprite.sprite = venue.backgroundSprite;
        description.text = venue.description;
        distance.text = "500 miles";
        capacity.text = venue.capacity.ToString();
        bookingFee.text = "£1000.00";

    }

    public async void ClickedGoButton()
    {
        BandManager.Instance.SetVenue(currentVenue);

        await CameraFade.Instance.DoCameraFade(1);
        SceneManager.LoadScene("Transit");
        Debug.Log($"Travelling to {currentVenue.name}");
        await Task.Delay(200);
    }

    public void ClickedCancelButton()
    {
        gameObject.SetActive(false);
    }
}
