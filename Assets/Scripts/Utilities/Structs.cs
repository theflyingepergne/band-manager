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

    public bool isSameDate(GameDate other)
    {
        return day == other.day && month == other.month && year == other.year;
    }
}
