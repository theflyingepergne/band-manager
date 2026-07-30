using UnityEngine.EventSystems;

public class ImageHoverInfo : UIHoverInfo
{
    private string prefix = "instrument_"; // Optional prefix to distinguish type

    public override void OnPointerEnter(PointerEventData eventData)
    {
        // Get index relative to its parent container
        int siblingIndex = transform.GetSiblingIndex();

        // Broadcasts "inst_0", "inst_1", etc. (or just the raw index string)
        TriggerHover($"{prefix}{siblingIndex}");
    }
}