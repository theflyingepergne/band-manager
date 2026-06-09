using UnityEngine;

public class DateManager
{
    public static DateManager Instance { get; private set; }

    //---Date---//
    public GameDate date = new GameDate(31, 1, 1979);

    //---Events---//
    public static System.Action<GameDate> OnDateChanged;

    //---Methods---//
    public static void Initialize()
    {
        Instance = new DateManager();
        Debug.Log("Initialized DateManager");
        // TODO: Load stats from SaveLoadSystem
        // Instance.LoadDate();
    }

    public void ChangeDate(int changeAmount)
    {
        GameDate _date = date;
        int currentMonthDays = MonthList.Months[_date.month].Days;

        _date.day += changeAmount;

        if (_date.day > currentMonthDays)
        {
            _date.month++;
            _date.day = 1;
        }

        if (_date.day < 1)
        {
            _date.month--;
            _date.day = MonthList.Months[_date.month].Days;
        }

        if (_date.month > 12)
        {
            _date.year++;
            _date.month = 1;
        }

        if (_date.month < 1)
        {
            _date.year--;
            _date.month = 12;
            _date.day = MonthList.Months[_date.month].Days;
        }

        date = _date;

        OnDateChanged?.Invoke(date);
    }
}
