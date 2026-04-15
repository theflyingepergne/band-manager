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
    [SerializeField] private GameObject currentSongPointer;

    //---Local References---//
    private GigSetlistUIManager gSUIM;
    private BandManager bm;

    private void Start()
    {
        // Init local refs
        bm = BandManager.Instance;
        gSUIM = gigSetlistUIManager.GetComponent<GigSetlistUIManager>();

        // Start gig
        StartCoroutine(RunGigSequence());
    }

    private IEnumerator RunGigSequence()
    {
        var setlist = BandManager.Instance.activeSetlist;

        for (int i = 0; i < setlist.Count; i++)
        {
            Debug.Log($"Now playing: {setlist[i].songName}");

            // 1. Update UI (Move the arrow)

            yield return new WaitForSeconds(songDuration);
        }

        // --- All Songs Finished ---

        // 3. Trigger "Crowd Wild" state
        // CrowdAnimator.SetTrigger("GoWild");
        // AudioSource.PlayOneShot(crowdCheerClip);

        yield return new WaitForSeconds(postGigWait);

        // 4. Show the Report
        ShowGigReport();
    }

    private void ShowGigReport()
    {
        gigReportUI.SetActive(true);
        // Pass data to the report based on the setlist quality
    }
}
