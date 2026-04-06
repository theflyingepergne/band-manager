using UnityEngine;
using UnityEngine.UI;

public class PrepareSetlistSongManager : MonoBehaviour
{
    [SerializeField] private GameObject prepareSetlistSongPrefab;
    [SerializeField] private Button removeButton;

    public void ToggleRemoveButton()
    {
        CanvasGroup cg = removeButton.GetComponent<CanvasGroup>();

        // Canvas group initialized as visible and interactable
        cg.alpha = (cg.alpha == 1f )? 0f : 1f;
        cg.interactable = !cg.interactable;
    }

    public void RemoveSong()
    {
        Destroy(prepareSetlistSongPrefab);
    }
}