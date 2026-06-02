using TMPro;
using UnityEngine;

public class GigSetlistSongManager : MonoBehaviour
{
    [SerializeField] private TMP_Text songNo;
    [SerializeField] private TMP_Text songName;

    public void SetupSong(int songNoDisplay = 0, string songNameDisplay = "funny song")
    {
        songNo.text = $"{songNoDisplay}. ";
        songName.text = $"{songNameDisplay}";
    }
}
