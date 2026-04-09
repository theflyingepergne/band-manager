using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PrepareSetlistSongManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{
    //---References---//
    [Header("Prefab References")]
    [SerializeField] private Button removeButton;

    [Header("Song details")]
    [SerializeField] private TMP_Text songNo;
    [SerializeField] private string songNoDefault = "1. ";
    [SerializeField] private TMP_Text songName;
    [SerializeField] private string songNameDefault = "A Little Less 16 Mandibles a Little More Munch";


    private LayoutElement layoutElement;
    private int startSiblingIndex;

    //---Methods---//
    void Start()
    {
        // Init text
        songNo.text = songNoDefault;
        songName.text = songNameDefault;

        // Init UI references
        layoutElement = GetComponent<LayoutElement>();
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
        layoutElement.ignoreLayout = true;

        startSiblingIndex = transform.GetSiblingIndex();

        Debug.Log($"Picked up song at index: {startSiblingIndex}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 1. Get the RectTransform of the song itself
        RectTransform rectTransform = GetComponent<RectTransform>();

        // 2. Translate Screen pixels to World coordinates
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector3 worldPoint))
        {
            // 3. Set the position!
            transform.position = worldPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        layoutElement.ignoreLayout = false;

        transform.SetSiblingIndex(startSiblingIndex);

        Debug.Log("Song snapped back to original slot.");
    }
}