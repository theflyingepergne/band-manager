using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EventNotification : MonoBehaviour
{  
    //---Local References---//
    Button button;
    Image image;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    public void SetupEventNotification(GameEventData data)
    {
        button.onClick.AddListener(() => GameEventManager.Instance.OnTriggerGameEvent(data));
        button.onClick.AddListener(() => DestroyNotification());

    }

    public void ShowNotification()
    {
        image.enabled = true;
    }

    private void DestroyNotification()
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(null);
        // TODO: tell schedule manager the event has been completed - maybe set date to 0/0/0?
        Destroy(gameObject);
    }
}
