using UnityEngine;
using UnityEngine.UI;

public class EventNotification : MonoBehaviour
{  
    //---Local References---//
    Button button;
    Image image;
    ScheduledEvent scheduledEvent;
    ScheduleManager sm;

    private void OnEnable()
    {
        sm = ScheduleManager.Instance;
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    public void SetupEventNotification(ScheduledEvent data)
    {
        scheduledEvent = data;
        button.onClick.AddListener(() => GameEventManager.Instance.OnTriggerGameEvent(data.gameEventData));
        button.onClick.AddListener(() => DestroyNotification());

    }

    public void ShowNotification()
    {
        image.enabled = true;
    }

    private void DestroyNotification()
    {
        scheduledEvent.isCompleted = true;
        gameObject.SetActive(false);
        gameObject.transform.SetParent(null);
        // TODO: tell schedule manager the event has been completed - maybe set date to 0/0/0?
        Destroy(gameObject);
    }
}
