using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTransit : MonoBehaviour, IClickable
{
    //---References---//
    [SerializeField] private GameObject setlistRoot;
    private PrepareSetlistManager sm;

    //---Methods---//
    void Start()
    {
        sm = setlistRoot.GetComponent<PrepareSetlistManager>();
    }

    public void OnClicked()
    {
        // Tell setlist to save its order to the bandmanager
        sm.FinalizeSetlist();

        VenueData venueToLoad = BandManager.Instance.destinationVenue;
        // eventually use venueToLoad to either load a scene
        // or use the venue data dress the scene
        // for now we just load Venue
        SceneManager.LoadScene("Venue");
    }
}
