[System.Serializable]
public class WriteSong : CustomGameEvent
{
    public override void Execute()
    {
        // setup song details
        string songName = "test";
        BandMemberInstance bandMember = null;
        GenreWeights genreWeights = null;
        float newSongScore = 0f;

        // create newSong
        SongEntry newSong = new
        (
            songName,
            bandMember,
            genreWeights,
            newSongScore
        );

        // add song to collection
        BandManager.Instance.AddSongToCollection(newSong);
    }
}