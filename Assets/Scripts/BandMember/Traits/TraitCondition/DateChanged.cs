[System.Serializable]
public class DateChanged : CustomTraitCondition
{
    private GameDate currentDate;
    private GameDate newDate;

    public override void Register()
    {
        CameraFade.OnFadeInComplete += HandleFadeInComplete;
        DateManager.OnDateChanged += HandleDateChanged;

        currentDate = DateManager.Instance.date;
        newDate = currentDate;
    }
    public override void Unregister()
    {
        CameraFade.OnFadeInComplete -= HandleFadeInComplete;
        DateManager.OnDateChanged -= HandleDateChanged;
    }

    private void HandleFadeInComplete()
    {
        if (!newDate.IsSameDate(currentDate))
        {
            traitInstanceOuter.ExecuteTraitLogic();
            currentDate = newDate;
        }
    }

    private void HandleDateChanged(GameDate newDate)
    {
        this.newDate = newDate;
    }
}