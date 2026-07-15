using System.Collections.Generic;
using UnityEngine;

public class BandManager
{
    public static BandManager Instance { get; private set; }

    //---References---//
    public Dictionary<string, BandMemberInstance> bandMembers = new();
    public List<SongEntry> songCollection = new();
    public List<SongEntry> activeSetlist = new();
    public VenueData destinationVenue;

    //---Local References---//
    private RecruitmentManager rm;

    //---Stats---//
    public float money = 100f;
    public float chemistry;
    public int fans;

    //---Events---//
    public static System.Action<StatType, float> OnStatChanged;
    public static System.Action<BandMemberInstance> OnBandMemberRecruitmentChanged;

    public static void Initialize()
    {
        Instance = new BandManager();
        Debug.Log("Initialized BandManager");
        // TODO: Load stats from SaveLoadSystem
        // Instance.LoadCoreStats();

        Instance.rm = RecruitmentManager.Instance;
    }

    //---Stat Methods---//
    public void ApplyStatChange(StatChange effect)
    {
        switch (effect.stat)
        {
            case StatType.Money:
                money += effect.amount;
                OnStatChanged?.Invoke(effect.stat, money);
                break;
            case StatType.Chemistry:
                chemistry = Mathf.Clamp(chemistry + effect.amount, 0f, 100f);
                OnStatChanged?.Invoke(effect.stat, chemistry);
                break;
            case StatType.Fans:
                fans += (int)effect.amount;
                OnStatChanged?.Invoke(effect.stat, fans);
                break;
        }

        // Debug.Log($"{effect.stat} changed by {effect.amount}!");
        
    }

    //---Band Member Methods---//
    public void RecruitMember(string id)
    {
        // Try get bandMemberInstance from RecruitmentManager
        BandMemberInstance member = rm.GetBandMemberInstance(id);

        if (member != null)
        {
            // if they exist, recruit them
            member.isRecruited = true;
            member.wasRecruited = true;

            bandMembers.Add(id, member);
            OnBandMemberRecruitmentChanged?.Invoke(member);

            Debug.Log($"Hired {member.name}!");
        }
        else
        {
            Debug.LogError("No BandMemberInstance found");
        }
    }

    //---Song Methods---//
    public void AddSongToCollection(SongEntry song)
    {
        songCollection.Add(song);
        // Debug.Log($"Added {song.name}, score = {song.score}");
    }

    //---Game Event Methods---//
    public void GenerateRandomSongFromEvent()
    {
        // Init vars
        string songName;
        BandMemberInstance bandMember;
        GenreWeights genreWeights;
        float newSongScore = Random.Range(60f, 100f);

        if (bandMembers != null && bandMembers.Count > 0)
        {
            // pick a random band member to supply song data
            var (id, member) = rm.GetRandomBandMemberInstance(null, bandMembers).Value;
            bandMember = member;
            genreWeights = bandMember.genreAffinities;
            songName = $"{bandMember.name}'s song";
        }
        else
        {
            songName = "Default song name";
            bandMember = new();
            genreWeights = new((Genre.Rock, 10f));
        }

        SongEntry newSong = new(
            songName,
            bandMember,
            genreWeights,
            newSongScore
        );
        
        AddSongToCollection(newSong);
    }

    //---Setlist Methods---//
    public List<SongEntry> PrepareSetlist()
    {
        if (songCollection.Count > 0)
        {
            // init temporary lists of songEntries
            List<SongEntry> selectedSongs = new();
            List<SongEntry> shuffled = new(songCollection);

            // shuffle the elements in the 'shuffled' list
            for (int i = 0; i < shuffled.Count; i++)
            {
                int randIndex = Random.Range(i, shuffled.Count);

                SongEntry temp = shuffled[i];
                shuffled[i] = shuffled[randIndex];
                shuffled[randIndex] = temp;
            }

            // pick max 6 SongEntry from the 'shuffled' list
            for (int i = 0; i < 6 && i < shuffled.Count; i++)
            {
                selectedSongs.Add(shuffled[i]);
            }
            activeSetlist = selectedSongs;
        }

        return activeSetlist;
    }

    //---Venue Methods---//
    public void SetVenue(VenueData chosenVenue)
    {
        if (chosenVenue != null)
        {
            destinationVenue = chosenVenue;
            // Debug.Log("venue data set!");
        }
    }
}