using UnityEngine;
using UnityEngine.UI;

public class VenueManager : Singleton<VenueManager>
{
    //---References---//
    [Header("UI")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Image venueBackground;
    public VenueData venueData;

    //---Local References---//

    private void OnEnable() => CameraFade.OnFadeInComplete += HandleFadeInComplete;
    private void OnDisable() => CameraFade.OnFadeInComplete -= HandleFadeInComplete;

    private void Start()
    {
        if (BandManager.Instance.destinationVenue != null)
        {
            venueData = BandManager.Instance.destinationVenue;
            venueBackground.sprite = venueData.backgroundSprite;
        }
    }

    // After fade in, should we: start gig, talk to venue owner or watch another band play?
    private void HandleFadeInComplete()
    {
        var (anyGigs, gig) = ScheduleManager.Instance.CheckAnyGigsOnDay(DateManager.Instance.date);

        if (anyGigs)
        {
            // start gig
            GigSetlistUIManager.Instance.SetupGigUI();
            ScheduleManager.Instance.MarkGigAsComplete(gig);
        }
        else // if
        {
            // start book future gig dialogue
            dialogueManager.BeginDialogue();
            // Debug.Log("No gig today");
        }
        // else watch band
    }
}