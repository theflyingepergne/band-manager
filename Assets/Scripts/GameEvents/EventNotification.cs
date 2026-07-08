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
        button.onClick.AddListener(() => GameEventManager.Instance.OnTriggerGameEvent(data));
        button.onClick.AddListener(() => DestroyNotification());
    }

    public void ShowNotification()
    {
        image.enabled = true;
    }

    private void DestroyNotification()
    {
        sm.MarkEventAsComplete(scheduledEvent);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(null);
        Destroy(gameObject);
    }
}