using UnityEngine;

public class VenueManager : Singleton<VenueManager>
{
    //---References---//
    [SerializeField] private DialogueManager dialogueManager;

    private void OnEnable() => CameraFade.OnFadeInComplete += HandleFadeInComplete;
    private void OnDisable() => CameraFade.OnFadeInComplete -= HandleFadeInComplete;

    // After fade in, should we: start gig, talk to venue owner or watch another band play?
    private void HandleFadeInComplete()
    {
        var (anyGigs, gig) = ScheduleManager.Instance.CheckAnyGigsToday();

        if (anyGigs)
        {
            // start gig
            GigSetlistUIManager.Instance.SetupGigUI();
            gig.isCompleted = true;
        }
        else // if
        {
            // start book future gig dialogue
            dialogueManager.BeginDialogue();
            Debug.Log("No gig today");
        }
        // else watch band
    }
}