using System.Collections;
using UnityEngine;

public class GigDirector : Singleton<GigDirector>
{
    //---References---//
    [Header("Config")]
    [SerializeField] private float songDuration = 5f;
    [SerializeField] private float postGigWait = 10f;

    [Header("UI References")]
    [SerializeField] private GameObject gigReportUI;
    [SerializeField] private GameObject gigSetlistUIManager;

    //---Local References---//
    private BandManager bm;

    private void Start()
    {
        // Init local refs
        bm = BandManager.Instance;
    }

    public void StartGig()
    {
        StartCoroutine(RunGigSequence());
    }

    private IEnumerator RunGigSequence()
    {

        for (int i = 0; i < bm.activeSetlist.Count; i++)
        {
            // var currentSong = setlist[i];
            // Debug.Log($"Now playing: {currentSong.songName}");

            // Update UI (Move the arrow)
            gigSetlistUIManager.GetComponent<GigSetlistUIManager>().HighlightCurrentSong(i);

            yield return new WaitForSeconds(songDuration);
        }

        //---Songs Finished---//

        // Trigger "Crowd Wild" state for a few seconds
        Debug.Log("Woo!");
        CrowdReaction();

        yield return new WaitForSeconds(postGigWait);

        // Show the Report
        ShowGigReport();
    }

    private void CrowdReaction()
    {
        CrowdMember[] crowd = FindObjectsByType<CrowdMember>(FindObjectsSortMode.None);
        foreach (var person in crowd)
        {
            person.StartJumping();
        }
    }

    private void ShowGigReport()
    {
        gigReportUI.SetActive(true);
        // Pass data to the report based on the setlist quality
    }
}
