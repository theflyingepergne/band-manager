[System.Serializable]
public class SongEntry
{
    public string name = "Default Song Name";
    public BandMemberInstance author = null;
    public GenreWeights genreWeights = null;
    
    public float score = 0f;
    // Eventually songScore will be a calculated value
    // Add things here that will be used to calculate songScore

    //---Constructor---//
    public SongEntry(
        string name = "Default Song Name",
        BandMemberInstance data = null,
        GenreWeights weights = null,
        float score = 0f
        )
    {
        this.name = name;
        author = data;
        genreWeights = weights;
        this.score = score;
    }
}