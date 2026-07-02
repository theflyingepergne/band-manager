using System.Threading.Tasks;
using UnityEngine;

public class EndDay : MonoBehaviour, IClickable
{
    //---References---//
    [SerializeField] private GameObject canvasEndDay;

    //---Local References---//
    private bool doChangeDate = false;

    //---Events---//
    private void OnEnable() => CameraFade.OnFadeOutComplete += HandleFadeOutComplete;
    private void OnDisable() => CameraFade.OnFadeOutComplete -= HandleFadeOutComplete;

    //---Methods---//
    public void OnClicked()
    {
        if (canvasEndDay.activeSelf != true)
        {
            // Debug.Log("End day?");
            canvasEndDay.SetActive(true);
        }
    }

    public void ClickedEndDay()
    {
        doChangeDate = true;
        canvasEndDay.SetActive(false);
        CameraFade.Instance.FadeInFadeOut();
    }

    private void HandleFadeOutComplete()
    {
        if (doChangeDate)
        {
            DateManager.Instance.ChangeDate(1);
            doChangeDate = false;
        }
    }

    public void ClickedCancel()
    {
        canvasEndDay.SetActive(false);
    }
}
