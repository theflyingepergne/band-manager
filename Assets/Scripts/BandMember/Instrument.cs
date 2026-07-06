using UnityEngine;

public class Instrument : MonoBehaviour
{
    [SerializeField] private InstrumentData instrumentData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InstrumentData data = instrumentData;
        Sprite sprite = data.instrumentSprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
