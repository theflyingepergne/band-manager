// The gig report shows the outcomes of the gig and gains
// This script calculates the gains as well as formatting the strings that will appear
using TMPro;
using UnityEngine;

public class GigReport : MonoBehaviour
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private TMP_Text moneyEarned;
    [SerializeField] private TMP_Text fansGained;
    [SerializeField] private TMP_Text buzzChange;
    [SerializeField] private TMP_Text chemistryChange;

    //---Local References---//
    private BandManager bm;
    private GigSetlistUIManager gSUIM;
    private VenueData vd;
    private float gigScore;
    private float songScoreTotal = 0f;

    public void ShowGigReport()
    {
        bm = BandManager.Instance;
        vd = bm.destinationVenue;
        gSUIM = GigSetlistUIManager.Instance;


        // Set text that will be displayed
        moneyEarned.text = GetMoneyGain();
        fansGained.text = GetFansGain();
        chemistryChange.text = GetChemistryGain();

        Debug.Log("Gig Score: " + GetGigScore());

    }

    private float GetSongScoreTotal()
    {
        if (gSUIM.setlist != null)
        {
            songScoreTotal = 0f;

            foreach (SongEntry song in gSUIM.setlist)
            {
                // Add song score to song score total
                songScoreTotal += song.songScore;
            }
            return songScoreTotal;
        }
        else
        {
            songScoreTotal = 0f;
            return songScoreTotal;
        }

    }

    private float GetGigScore()
    {
        float numSongs = gSUIM.setlist.Count;

        // The gig score calculation needs to take way more stuff into account e.g
        // Venue should have a preferred number of songs
        // Playing 1 song at a venue where they expect 6 should be punishing
        // Eventually factor in genre matching too
        // Some kind of "energy level" calculation based on setlist order
        // Traits
        if (numSongs > 0)
        {
            gigScore = GetSongScoreTotal() / (numSongs * 100f);

            return gigScore;
        }
        else
        {
            // To prevent dividing by 0
            // If there are no songs in the setlist, gigScore =  0
            // Really, we should have warned the player before they got to this point
            gigScore = 0f;
            return gigScore;
        }

    }

    private string GetMoneyGain()
    {
        // Calculate money gain
        float moneyGain = vd.basePay + (GetGigScore() * vd.basePay);

        // Apply stat change
        StatChange moneyStatChange = new StatChange();
        moneyStatChange.SetupStatChange(StatType.Money, moneyGain);
        moneyStatChange.ApplyStatChange();

        return moneyGain.ToString("+£0.00;-£0.00");
    }

    private string GetFansGain()
    {
        // Calculate fan gain
        // Buzz should eventually be taken into account
        // High buzz = more fans multiplier
        // For now just multiply venue capacity by gig score
        float fansGain = Mathf.Ceil(vd.capacity * GetGigScore());

        // Apply stat change
        StatChange fansStatChange = new StatChange();
        fansStatChange.SetupStatChange(StatType.Fans, fansGain);
        fansStatChange.ApplyStatChange();

        return fansGain.ToString("+0 new fans;-0 fans");
    }

    private string GetChemistryGain()
    {
        float numSongs = gSUIM.setlist.Count;

        // Calculate chemistry gain
        // Bands that play more songs gain more chemistry
        // Potentially to the chagrin of the venue's preferred song count
        float chemistryGain = GetGigScore() * numSongs;

        // Apply stat change
        StatChange chemistryStatChange = new StatChange();
        chemistryStatChange.SetupStatChange(StatType.Chemistry, chemistryGain);
        chemistryStatChange.ApplyStatChange();

        return chemistryGain.ToString("+0.#;-0.#") + "%";
    }
}
