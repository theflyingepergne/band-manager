using UnityEngine;

public class VenueManager : Singleton<VenueManager>
{
    ScheduleManager sm;
    // is it gig day?
    // 
    // if yes, start gig
    // if no, show bartender and start dialogue
    private void Start()
    {
        sm = ScheduleManager.Instance;
        
        // GigSetlistUIManager.Instance.SetupGigUI();


        if (sm.CheckGigToday() == true)
        {
            // start gig
            Debug.Log("It's gig day!");
            GigSetlistUIManager.Instance.SetupGigUI();

        }
        else
        {
            // book future gig
            Debug.Log("No gig today");
        }
        // else watch band
    }
}
