using TMPro;
using UnityEngine;

public class SongWrapperManager : MonoBehaviour
{
    [SerializeField] private TMP_Text songNo;
    [SerializeField] private TMP_Text songName;

    public void SetSongNo(int songNoDisplay=0)
    {
        songNo.text = $"{songNoDisplay}. ";
    }
    
    public void SetSongName(string songNameDisplay="funny song")
    {
        songName.text = songNameDisplay;
    }
}
