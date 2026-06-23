using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class VenueInfoPanel : MonoBehaviour
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private RectTransform venueInfoBorder;
    [SerializeField] private TMP_Text venueName;
    [SerializeField] private Image venueSprite;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text distance;
    [SerializeField] private TMP_Text capacity;
    [SerializeField] private TMP_Text bookingFee;

    private VenueData currentVenue;

    //---EVents---//
    void OnEnable() => ClickScript.OnClickEmptySpace += HandleClickOnEmptySpace;
    void OnDisable() => ClickScript.OnClickEmptySpace -= HandleClickOnEmptySpace;

    //---Methods---//
    public void SetPanelPosition(bool isOnLeftHandSide)
    {
        if (isOnLeftHandSide == true)
        {
            // put venueInfoBorder on right hand side
            venueInfoBorder.anchorMin = new Vector2(1, 0);
            venueInfoBorder.anchorMax = new Vector2(1, 0);
            venueInfoBorder.pivot = new Vector2(1, 0);
            venueInfoBorder.anchoredPosition = new Vector2(-50f, 50f);
        }
        else
        {
            // put venueInfoBorder on left hand side
            venueInfoBorder.anchorMin = Vector2.zero;
            venueInfoBorder.anchorMax = Vector2.zero;
            venueInfoBorder.pivot = new Vector2(0, 0);
            venueInfoBorder.anchoredPosition = new Vector2(50f, 50f);
        }
    }

    public void SetupVenueInfo(VenueData venue)
    {
        currentVenue = venue;
        venueName.text = venue.name;
        venueSprite.sprite = venue.backgroundSprite;
        description.text = venue.description;
        distance.text = "Distance: " + venue.distance.ToString("0.##") + "miles";
        capacity.text = "Capacity: " + venue.capacity.ToString();
        bookingFee.text = "Booking Fee: " + (venue.bookingFee > 0 ? venue.bookingFee.ToString("£#,##0.00") : "Free");
    }

    public async void ClickedGoButton()
    {
        BandManager.Instance.SetVenue(currentVenue);
        var (anyGigs, gig) = ScheduleManager.Instance.CheckAnyGigsOnDay(DateManager.Instance.date);

        await CameraFade.Instance.DoCameraFade(1);

        if (anyGigs)
        {
            SceneManager.LoadScene("Transit");
        }
        else
        {
            SceneManager.LoadScene("Venue");
            // Debug.Log($"Travelling to {currentVenue.name}");
        }
    }

    public void CloseVenueInfoPanel()
    {
        gameObject.SetActive(false);
    }

    private void HandleClickOnEmptySpace()
    {
        CloseVenueInfoPanel();
    }
}
