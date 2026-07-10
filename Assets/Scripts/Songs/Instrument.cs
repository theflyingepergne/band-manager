using UnityEngine;

public class Instrument: MonoBehaviour
{
    //---Local References---//
    private InstrumentInstance instrumentInstance;

    //---Methods---//
    void SetupInstrument(InstrumentInstance data)
    {
        instrumentInstance = data;
        Sprite sprite = instrumentInstance.sprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}