using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PrepareSetlistSongManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler

{
    //---References---//
    [Header("Prefab References")]
    [SerializeField] private Button removeButton;

    [Header("Song details")]
    [SerializeField] private TMP_Text songNo;
    [SerializeField] private TMP_Text songName;
    [SerializeField] private string songNameDefault = "A Little Less 16 Mandibles a Little More Munch";
    public SongEntry song;

    private CanvasGroup canvasGroup;
    private LayoutElement layoutElement;
    private int startSiblingIndex;

    //---Events---//
    void OnEnable() => PrepareSetlistManager.OnSetlistReordered += RefreshVisualIndex;
    void OnDisable() => PrepareSetlistManager.OnSetlistReordered -= RefreshVisualIndex;

    //---Methods---//
    void Start()
    {
        // Init UI references
        layoutElement = GetComponent<LayoutElement>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetupSong(SongEntry data)
    {
        song = data;
        // Init text
        startSiblingIndex = transform.GetSiblingIndex() + 1;
        songNo.text = $"{startSiblingIndex}. ";

        if (data != null)
        {
            songName.text = data.songName;
        }
        else
        {
            songName.text = songNameDefault;
        }
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
        transform.SetParent(null);
        Destroy(gameObject);
        PrepareSetlistManager.Instance.BroadcastReorder();
    }

    //---Song Reordering Methods---//
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Get target index from hovering over other songs in the setlist
        PrepareSetlistManager.Instance.SetCurrentHoverIndex(transform.GetSiblingIndex());
        PrepareSetlistManager.Instance.MoveGhostSong(transform.GetSiblingIndex());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        layoutElement.ignoreLayout = true;
        canvasGroup.blocksRaycasts = false;

        startSiblingIndex = transform.GetSiblingIndex();
        PrepareSetlistManager.Instance.EnableGhostSong(startSiblingIndex);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Translate Screen pixels to World coordinates
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector3 worldPoint))
        {
            transform.position = worldPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Get the last index hovered over from the manager
        int targetIndex = PrepareSetlistManager.Instance.GetCurrentHoverIndex();

        // If we hovered over a valid slot, move there
        if (targetIndex != -1)
        {
            transform.SetSiblingIndex(targetIndex);
        }

        layoutElement.ignoreLayout = false;
        canvasGroup.blocksRaycasts = true;

        PrepareSetlistManager.Instance.SetCurrentHoverIndex(-1);
        songNo.text = $"{targetIndex + 1}. ";

        PrepareSetlistManager.Instance.DisableGhostSong();
        PrepareSetlistManager.Instance.BroadcastReorder();
    }

    private void RefreshVisualIndex()
    {
        // 'transform.GetSiblingIndex()' is always up-to-date reorder
        int newIndex = transform.GetSiblingIndex() + 1;
        songNo.text = $"{newIndex}. ";
    }
}