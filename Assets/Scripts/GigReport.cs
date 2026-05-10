using TMPro;
using UnityEngine;
using System.Collections.Generic;

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
    private VenueData vd;
    private float gigScore;
    private float songScoreTotal = 0f;

    void OnEnable()
    {
        bm = BandManager.Instance;
        vd = bm.destinationVenue;

        GetGigScore();
        moneyEarned.text = GetMoneyGain();
    }

    private float GetSongScoreTotal()
    {
        foreach (SongEntry song in bm.activeSetlist)
        {
            // Add song score to song score total
            songScoreTotal += song.songScore;
        }
        return songScoreTotal;
    }

    private float GetGigScore()
    {
        float numSongs = bm.activeSetlist.Count;
        gigScore = GetSongScoreTotal() / (numSongs * 100f);
        return gigScore;
    }

    private string GetMoneyGain()
    {
        // Calculate money gain
        float moneyGain = vd.basePay + (GetGigScore() * vd.basePay);

        // Apply stat change
        StatChange moneyStatChange = new StatChange();
        moneyStatChange.SetupStatChange(StatType.Money, moneyGain);
        moneyStatChange.ApplyStatChange();

        return moneyGain.ToString("+£#.##;-£#.##");
    }


}
