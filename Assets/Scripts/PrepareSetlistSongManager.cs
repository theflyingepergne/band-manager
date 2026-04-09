using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PrepareSetlistSongManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{
    //---References---//
    [Header("Manager References")]

    [Header("Prefab References")]
    [SerializeField] private Button removeButton;

    [Header("Song details")]
    [SerializeField] private TMP_Text songNo;
    [SerializeField] private string songNoDefault = "1. ";
    [SerializeField] private TMP_Text songName;
    [SerializeField] private string songNameDefault = "A Little Less 16 Mandibles a Little More Munch";

    //---Methods---//
    void Start()
    {
        songNo.text = songNoDefault;
        songName.text = songNameDefault;
    }

    //---Button Methods---//
    public void ToggleRemoveButton()
    {
        CanvasGroup cg = removeButton.GetComponent<CanvasGroup>();

        cg.alpha = (cg.alpha == 1f) ? 0f : 1f;
        cg.interactable = !cg.interactable;
    }

    public void RemoveSong()
    {
        Destroy(gameObject);
    }

    //---Drag and Drop Methods---//
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("beginning drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging: " + eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Ended drag");
    }
}