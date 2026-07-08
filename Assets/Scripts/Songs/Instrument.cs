using UnityEngine;

public class Instrument: MonoBehaviour
{
    //---References---//
    public string id;

    //---Local References---//
    private InstrumentInstance data;

    //---Methods---//
    void Start()
    {
        SetupInstrument(id);
    }

    void SetupInstrument(string id)
    {
        data = InstrumentManager.Instance.GetInstrumentInstance(id);
        Sprite sprite = data.sprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}