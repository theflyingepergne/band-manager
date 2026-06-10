using System.Threading.Tasks;
using UnityEngine;

public class EndDay : MonoBehaviour, IClickable
{
    //---References---//
    [SerializeField] private GameObject canvasEndDay;

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
        // Debug.Log("Ended day");
        canvasEndDay.SetActive(false);
        // Debug.Log($"Current Date: {DateManager.Instance.date.GetDateAsString()}");
        FadeInFadeOut();
    }

    public void ClickedCancel()
    {
        canvasEndDay.SetActive(false);
    }

    private async void FadeInFadeOut()
    {
        // Fade to black
        await CameraFade.Instance.DoCameraFade(1f);

        // Use Task.Delay (milliseconds) to hold
        await Task.Delay(500);
        
        DateManager.Instance.ChangeDate(1);

        // Fade back in
        await CameraFade.Instance.DoCameraFade(0f);
    }
}
