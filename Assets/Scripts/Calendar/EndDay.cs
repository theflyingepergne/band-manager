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
            Debug.Log("End day?");
            canvasEndDay.SetActive(true);
        }
    }

    public void ClickedEndDay()
    {
        Debug.Log("Ended day");
        DateManager.Instance.ChangeDate(1);
        canvasEndDay.SetActive(false);
        Debug.Log($"Current Date: {DateManager.Instance.GetDate()}");
    }

    public void ClickedCancel()
    {
        canvasEndDay.SetActive(false);
    }
}
