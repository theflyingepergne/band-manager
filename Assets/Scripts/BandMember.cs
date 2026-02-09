using UnityEngine;

public class BandMember : MonoBehaviour
{
    [SerializeField] ScriptableObject bandMemberData;
    
    void Start()
    {
        BandMemberData data = bandMemberData as BandMemberData;
        Sprite sprite = data.memberSprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
