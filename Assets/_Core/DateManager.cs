using UnityEngine;

public class DateManager
{
    public static DateManager Instance { get; private set; }

    //---Date---//
    public int day = 31;
    public int month = 1;
    public int year = 1979;

    //---Events---//
    public static System.Action<int, int, int> OnDateChanged;

    public static void Initialize()
    {
        Instance = new DateManager();
        Debug.Log("Initialized DateManager");
        // TODO: Load stats from SaveLoadSystem
        // Instance.LoadDate();
    }

    public void ChangeDate(int changeAmount)
    {
        int currentMonthDays = MonthList.Months[month].Days;
        day += changeAmount;

        // if day is higher than currentMonthDays, increment month
        if (day > currentMonthDays)
        {
            month++;
            day = 1;
        }

        // if day is less than 1, decrement month
        if (day < 1)
        {
            month--;
            day = MonthList.Months[month].Days;
        }

        // if month is greater than 12, increment year
        if (month > 12)
        {
            year++;
            month = 1;
        }

        // if month is less than 1, decrement year
        if (month < 1)
        {
            year--;
            month = 12;
            day = MonthList.Months[month].Days;
        }

        OnDateChanged?.Invoke(day, month, year);
    }

    public string GetDate()
    {
        return $"{day}/{month}/{year}";
    }

}
