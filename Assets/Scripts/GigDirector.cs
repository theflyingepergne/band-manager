using System.Collections;
using System.Collections.Generic;
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
    private List<GameObject> vibeBars = new List<GameObject>();
    private float gigScore;
    private float songScoreTotal = 0f;

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

            // Fill current vibeBar
            vibeBars[i].GetComponent<GigVibeBar>().FillBar();

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
        Debug.Log("Gig score: " + GetGigScore());
    }

    //---Vibe Bar---//
    public void SetupAllVibeBars()
    {
        vibeBars.Clear();

        foreach (SongEntry song in gSUIM.setlist)
        {
            SetupVibeBar(song);

            // Add song score to song score total
            songScoreTotal += song.songScore;
        }

    }

    public GameObject SetupVibeBar(SongEntry song)
    {
        // Create vibeBar and add to vibeBars
        GameObject vibeBar = Instantiate(vibeBarPrefab, vibeBarWrapper, false);
        vibeBar.transform.localPosition = Vector3.zero;
        vibeBars.Add(vibeBar);

        // Setup vibeBar
        GigVibeBar vibeBarScript = vibeBar.GetComponent<GigVibeBar>();
        vibeBarScript.SetupBar(song.songScore, GigDirector.Instance.songDuration);

        // Debug.Log(song.songName);
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
        // TODO Pass data to the report based on the setlist quality
    }

    private float GetGigScore()
    {
        float numSongs = gSUIM.setlist.Count;
        gigScore = songScoreTotal / (numSongs * 100f);
        return gigScore;
    }
}
