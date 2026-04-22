using System.Collections;
using UnityEngine;

public class GigDirector : Singleton<GigDirector>
{
    //---References---//
    [Header("Config")]
    [SerializeField] public float songDuration = 5f;
    [SerializeField] private float postGigWait = 10f;
    [SerializeField] private float crowdReactionPercentage = 60f;
    private float percentage = 0;

    [Header("UI References")]
    [SerializeField] private GameObject gigReportUI;
    [SerializeField] private RectTransform vibeBarWrapper;

    [Header("Prefabs")]
    [SerializeField] private GameObject vibeBarPrefab;


    //---Local References---//
    private BandManager bm;
    private GigSetlistUIManager gSUIM;

    private void Start()
    {
        // Init local refs
        bm = BandManager.Instance;
        gSUIM = GigSetlistUIManager.Instance;
    }

    public void StartGig()
    {
        StartCoroutine(RunGigSequence());
    }

    private IEnumerator RunGigSequence()
    {
        int i = 0;

        foreach (SongEntry song in gSUIM.setlist)
        {
            // Update UI (Move the arrow)
            gSUIM.GetComponent<GigSetlistUIManager>().HighlightCurrentSong(i);

            // Create vibeBar for current song
            // vibeBar fills itself on setup using song params
            SetupVibeBar(song);

            // "Wait" for song to end
            yield return new WaitForSeconds(songDuration);

            i++;
        }

        //---Songs Finished---//

        // Trigger "Crowd Wild" state for a few seconds
        // Debug.Log("Woo!");

        CrowdReaction(PercentageCrowdReaction());

        yield return new WaitForSeconds(postGigWait);

        // Show the Report
        ShowGigReport();
    }

    //---Vibe Bar---//
    public GameObject SetupVibeBar(SongEntry song)
    {
        GameObject vibeBar = Instantiate(vibeBarPrefab, vibeBarWrapper, false);
        GigVibeBar vibeBarScript = vibeBar.GetComponent<GigVibeBar>();
        vibeBar.transform.localPosition = Vector3.zero;

        vibeBarScript.SetupBar(song.songScore, GigDirector.Instance.songDuration);

        Debug.Log(song.songName);
        return vibeBar;
    }

    //---Crowd Reaction---//
    private float PercentageCrowdReaction()
    {
        // TODO calculate crowd reaction percentage based on song stats
        percentage = crowdReactionPercentage / 100f;

        return percentage;
    }

    public void CrowdReaction(float percentage)
    {
        // Clamp percentage between 0 and 1 just in case
        float weight = Mathf.Clamp01(percentage);

        CrowdMember[] crowd = FindObjectsByType<CrowdMember>(FindObjectsSortMode.None);

        foreach (var person in crowd)
        {
            // Random.value returns a float between 0.0 and 1.0
            if (UnityEngine.Random.value <= weight)
            {
                person.StartJumping();
            }
        }
    }

    //---Gig Report---//
    private void ShowGigReport()
    {
        gigReportUI.SetActive(true);
        // Pass data to the report based on the setlist quality
    }
}
