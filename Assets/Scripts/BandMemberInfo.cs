using UnityEngine;

public class BandMemberInfo : MonoBehaviour
{
    [SerializeField] ScriptableObject bandMemberData;
    
    void Start()
    {
        BandMemberData data = bandMemberData as BandMemberData;
        Sprite sprite = data.memberSprite;
        GetComponent<SpriteRenderer>().sprite = sprite;   
    }
}
