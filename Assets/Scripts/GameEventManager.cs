using TMPro;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    //---References---//
    [Header("GameEventData")]
    [SerializeField] private GameEventData gameEventData;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI eventTitle;
    [SerializeField] private TextMeshProUGUI eventDescription;
    [SerializeField] private GameObject eventSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventTitle.text = gameEventData.title;
        eventDescription.text = gameEventData.description;
        eventSprite.GetComponent<SpriteRenderer>().sprite = gameEventData.sprite;
    }
}
