using UnityEngine;

[System.Serializable]
public struct GameDate
{
    public int day;
    public int month;
    public int year;

    public GameDate(int day, int month, int year)
    {
        this.day = day;
        this.month = month;
        this.year = year;
    }

    // The logic lives here now, returning a fresh, valid GameDate
    public GameDate AddDays(int changeAmount)
    {
        // Copy current values to local variables for manipulation
        int d = day + changeAmount;
        int m = month;
        int y = year;

        // Forward overflow
        while (d > MonthList.Months[m].Days)
        {
            d -= MonthList.Months[m].Days;
            m++;

            if (m > 12)
            {
                y++;
                m = 1;
            }
        }

        // Backward overflow
        while (d < 1)
        {
            m--;

            if (m < 1)
            {
                y--;
                m = 12;
            }

            d += MonthList.Months[m].Days;
        }

        return new GameDate(d, m, y);
    }

    public bool isSameDate(GameDate other)
    {
        return day == other.day && month == other.month && year == other.year;
    }

    public readonly string GetDateAsString()
    {
        return $"{day}/{month}/{year}";
    }
}
