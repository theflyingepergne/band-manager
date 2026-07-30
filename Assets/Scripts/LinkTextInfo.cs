using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TMP_Text))]
public class LinkTextInfo : UIHoverInfo, IPointerMoveHandler
{
    private TMP_Text tmpText;
    private int lastLinkIndex = -1;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    public void OnPointerMove(PointerEventData pointerEventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(tmpText, Mouse.current.position.ReadValue(), Camera.main);

        if (linkIndex != -1)
        {
            if (linkIndex != lastLinkIndex)
            {
                if (lastLinkIndex != -1)
                {
                    SetLinkColor(lastLinkIndex, tmpText.color);
                }

                lastLinkIndex = linkIndex;
                TMP_LinkInfo linkInfo = tmpText.textInfo.linkInfo[linkIndex];

                // Pass the link ID up through base class
                TriggerHover(linkInfo.GetLinkID());
                SetLinkColor(linkIndex, Color.yellow);
            }
        }
        else if (lastLinkIndex != -1)
        {
            ResetHover();
        }
    }

    public override void OnPointerExit(PointerEventData pointerEventData)
    {
        ResetHover();
        base.OnPointerExit(pointerEventData);
    }

    private void ResetHover()
    {
        if (lastLinkIndex != -1)
        {
            SetLinkColor(lastLinkIndex, tmpText.color);
            lastLinkIndex = -1;
        }
    }

    private void SetLinkColor(int linkIndex, Color32 color)
    {
        TMP_LinkInfo linkInfo = tmpText.textInfo.linkInfo[linkIndex];

        for (int i = 0; i < linkInfo.linkTextLength; i++)
        {
            int characterIndex = linkInfo.linkTextfirstCharacterIndex + i;
            TMP_CharacterInfo charInfo = tmpText.textInfo.characterInfo[characterIndex];

            if (!charInfo.isVisible) continue;

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Color32[] vertexColors = tmpText.textInfo.meshInfo[materialIndex].colors32;

            vertexColors[vertexIndex + 0] = color;
            vertexColors[vertexIndex + 1] = color;
            vertexColors[vertexIndex + 2] = color;
            vertexColors[vertexIndex + 3] = color;
        }

        tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}