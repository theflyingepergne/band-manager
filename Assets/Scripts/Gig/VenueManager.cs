using UnityEngine;

public class VenueManager : Singleton<VenueManager>
{
    // is it gig day?
    // 
    // if yes, start gig
    // if no, show bartender and start dialogue
    private void Start()
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
            // book future gig
            Debug.Log("No gig today");
        }
        // else watch band
    }
}
