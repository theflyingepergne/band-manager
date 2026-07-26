using UnityEngine;

[System.Serializable]
public class DateChanged : CustomTraitCondition
{
    //---References---//
    [Header("Settings")]
    public bool doAfterFadeIn = true;

    //---Local References---//
    private GameDate currentDate;
    private GameDate newDate;

    public override void Register()
    {
        if (doAfterFadeIn)
        {
            // if doAfterFadeIn == true, listen for the event
            CameraFade.OnFadeInComplete += HandleFadeInComplete;
        }

        DateManager.OnDateChanged += HandleDateChanged;

        currentDate = DateManager.Instance.date;
        newDate = currentDate;
    }
    public override void Unregister()
    {
        if (doAfterFadeIn)
        {
            CameraFade.OnFadeInComplete -= HandleFadeInComplete;
        }

        DateManager.OnDateChanged -= HandleDateChanged;
    }

    private void HandleFadeInComplete()
    {
        // as we only listen for the event if doAfterFadeIn == true
        // no need to check here as this event won't be called if == false
        if (!newDate.IsSameDate(currentDate))
        {
            traitInstanceOuter.ExecuteTraitLogic();
            currentDate = newDate;
        }
    }

    private void HandleDateChanged(GameDate newDate)
    {
        if (doAfterFadeIn)
        {
            this.newDate = newDate;
        }
        else
        {
            traitInstanceOuter.ExecuteTraitLogic();

            this.newDate = newDate;
            currentDate = newDate;
        }
    }
}