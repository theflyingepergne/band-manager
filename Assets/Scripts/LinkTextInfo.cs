using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TMP_Text))]
public class LinkTextInfo : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler
{
    //---References---//
    private TMP_Text tmpText;
    private int lastLinkIndex = -1;

    //---Events---//
    public static event System.Action<string> OnLinkHovered;
    public static event System.Action OnLinkExited;

    //---Methods---//
    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    public void OnPointerMove(PointerEventData pointerEventData)
    {
        // on move, get the link we are hovering over
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(tmpText, Mouse.current.position.ReadValue() , Camera.main);

        if (linkIndex != -1)
        {
            if (linkIndex != lastLinkIndex)
            {
                lastLinkIndex = linkIndex;

                TMP_LinkInfo linkInfo = tmpText.textInfo.linkInfo[linkIndex];

                OnLinkHovered?.Invoke(linkInfo.GetLinkID());
            }
        }
        else
        {
            ClearHover();
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        ClearHover();
    }

    private void ClearHover()
    {
        lastLinkIndex = -1;
        OnLinkExited?.Invoke();
    }
}
