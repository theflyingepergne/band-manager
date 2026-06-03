using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

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

            // Test

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
        // await Task.Delay(200);
    }

    public void ClickedCancelButton()
    {
        gameObject.SetActive(false);
    }
}
